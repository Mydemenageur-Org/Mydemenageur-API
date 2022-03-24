
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Mydemenageur.DAL.Models.Stripe;
using Mydemenageur.DAL.Settings;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System;
using MongoDB.Driver;
using Mydemenageur.DAL.DP.Interface;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PaymentController: ControllerBase
    {
        private readonly IDPMyDemenageurUser _dpMyDemenageurUser;
        public readonly IOptions<StripeSettings> options;
        private readonly IStripeClient client;

        public PaymentController(IOptions<StripeSettings> options, IDPMyDemenageurUser dpMyDemenageurUser)
        {
            _dpMyDemenageurUser = dpMyDemenageurUser;
            this.options = options;
            this.client = new StripeClient(this.options.Value.StripePrivateKey);
        }

        [HttpGet("config")]
        public ConfigResponse GetConfig()
        {
            var options = new PriceListOptions
            {
                LookupKeys = new List<string>
              {
                "abonnement_intermediaire",
                "abonnement_premium",
                "abonnement_professionnel",
                "abonnement_business_pack"
              }
            };
            var service = new PriceService();
            var prices = service.List(options);
            // return json: publishableKey (.env)
            return new ConfigResponse
            {
                PublishableKey = this.options.Value.StripePublicKey,
                Prices = prices.Data
            };
        }

        [HttpPost("create-payment-intent")]
        public IActionResult CreatePaymentIntent([FromBody] CreatePaymentIntentRequest request)
        {
            var myDem = _dpMyDemenageurUser.GetUserById(request.UserId).FirstOrDefault();
            if (myDem == null)
                return NotFound("User not found");

            var customerId = myDem.StripeId;
            if (customerId == null)
            {
                var customerService = new CustomerService();
                customerId = customerService.Create(new CustomerCreateOptions
                {
                    Email = myDem.Email
                }).Id;
                myDem.StripeId = customerId;
                _dpMyDemenageurUser.GetCollection().ReplaceOne(u => u.Id == request.UserId, myDem);
            }

            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = CalculateOrderAmount(request.Items),
                Currency = request.Currency,
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true
                },
                Customer = customerId
            });

            return Ok(new { clientSecret = paymentIntent.ClientSecret });
        }

        [HttpPost("create-customer")]
        public IActionResult CreateCustomer([FromBody] CreateCustomerRequest req)
        {
            var options = new CustomerCreateOptions
            {
                Email = req.Email,
            };
            var service = new CustomerService();
            var customer = service.Create(options);

            // Set the cookie to simulate an authenticated user.
            // In practice, this customer.Id is stored along side your
            // user and retrieved along with the logged in user.
            HttpContext.Response.Cookies.Append("customer", customer.Id);

            return Ok(new CreateCustomerResponse
            {
                Customer = customer,
            });
        }

        [HttpPost("create-subscription")]
        public IActionResult CreateSubscription([FromBody] CreateSubscriptionRequest req)
        {
            var myDem = _dpMyDemenageurUser.GetUserById(req.MyDemUserId).FirstOrDefault();
            if (myDem == null)
                return NotFound("User not found");

            var customerId = myDem.StripeId;
            if (customerId == null)
            {
                var customerService = new CustomerService();
                customerId = customerService.Create(new CustomerCreateOptions
                {
                    Email = myDem.Email
                }).Id;
                myDem.StripeId = customerId;
                _dpMyDemenageurUser.GetCollection().ReplaceOne(u => u.Id == req.MyDemUserId, myDem);
            }

            // Create subscription
            var subscriptionOptions = new SubscriptionCreateOptions
            {
                Customer = customerId,
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                    {
                        Price = req.PriceId,
                    },
                },
                PaymentBehavior = "default_incomplete",
            };
            subscriptionOptions.AddExpand("latest_invoice.payment_intent");
            var subscriptionService = new SubscriptionService();
            try
            {
                Subscription subscription = subscriptionService.Create(subscriptionOptions);

                return Ok(new SubscriptionCreateResponse
                {
                    SubscriptionId = subscription.Id,
                    ClientSecret = subscription.LatestInvoice.PaymentIntent.ClientSecret,
                });
            }
            catch (StripeException e)
            {
                Console.WriteLine($"Failed to create subscription.{e}");
                return BadRequest();
            }
        }

        [HttpGet("subscriptions")]
        public IActionResult ListSubscriptions()
        {
            var customerId = HttpContext.Request.Cookies["customer"];
            var options = new SubscriptionListOptions
            {
                Customer = customerId,
                Status = "all",
            };
            options.AddExpand("data.default_payment_method");
            var service = new SubscriptionService();
            var subscriptions = service.List(options);

            return Ok(new SubscriptionsResponse
            {
                Subscriptions = subscriptions,
            });
        }

        [HttpPost("cancel-subscription")]
        public IActionResult CancelSubscription([FromBody] CancelSubscriptionRequest req)
        {
            var service = new SubscriptionService();
            var subscription = service.Cancel(req.Subscription, null);
            return Ok(new SubscriptionResponse
            {
                Subscription = subscription,
            });
        }

        [HttpPost("update-subscription")]
        public IActionResult UpdateSubscription([FromBody] UpdateSubscriptionRequest req)
        {
            var service = new SubscriptionService();
            var subscription = service.Get(req.Subscription);

            var options = new SubscriptionUpdateOptions
            {
                CancelAtPeriodEnd = false,
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                    {
                        Id = subscription.Items.Data[0].Id,
                        Price = Environment.GetEnvironmentVariable(req.NewPrice.ToUpper()),
                    }
                }
            };
            var updatedSubscription = service.Update(req.Subscription, options);
            return Ok(new SubscriptionResponse
            {
                Subscription = updatedSubscription,
            });
        }

        private long CalculateOrderAmount(Item[] items)
        {
            var service = new PriceService();
            long finalAmount = 0;

            foreach (var item in items)
            {
                finalAmount += service.Get(item.Id).UnitAmount ?? 0;
            }

            return finalAmount;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            Event stripeEvent;
            try
            {
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    this.options.Value.WebhookSecret
                );
                Console.WriteLine($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something failed {e}");
                return BadRequest();
            }

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                Console.WriteLine($"Session ID: {session.Id}");
                // Take some action based on session.
            }

            return Ok();
        }
    }
}
