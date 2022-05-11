
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.BillingPortal;
using Mydemenageur.DAL.Models.Stripe;
using Mydemenageur.DAL.Settings;
using Mydemenageur.BLL.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System;
using MongoDB.Driver;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models.Users;

namespace Mydemenageur.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PaymentController: ControllerBase
    {
        private readonly IDPMyDemenageurUser _dpMyDemenageurUser;
        private readonly IUsersService _usersService;
        public readonly IOptions<StripeSettings> options;
        private readonly IStripeClient client;

        public PaymentController(IOptions<StripeSettings> options, IDPMyDemenageurUser dpMyDemenageurUser, IUsersService usersService)
        {
            _dpMyDemenageurUser = dpMyDemenageurUser;
            _usersService = usersService;
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
            if (customerId == null || customerId == "")
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
            //var service = new PaymentMethodService();
            //var options = new PaymentMethodAttachOptions { Customer = subscriptionOptions.Customer };
            var subscriptionService = new SubscriptionService();
            try
            {
                Subscription subscription = subscriptionService.Create(subscriptionOptions);
                //service.Attach(subscription.LatestInvoice.PaymentIntent.PaymentMethod.ToString(), options);
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
        [HttpPost("create-customer-portal-session")]
        public string CustomerPortal([FromBody] CustomerPortal req)
        {

            // Authenticate your user.
            var options = new SessionCreateOptions
            {
                Customer = req.CustomerId,
                ReturnUrl = req.ReturnURL,
            };
            var service = new SessionService();
            var session = service.Create(options);

            return session.Url;
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

        [HttpGet("get-subscription/{id:length(24)}")]
        public IActionResult GetSubscriptionByCustomerId(string id)
        {
            try
            {
                var myDem = _dpMyDemenageurUser.GetUserById(id).FirstOrDefault();
                var options = new SubscriptionListOptions
                {
                    Customer = myDem.StripeId,
                    Status = "active",
                };
                var service = new SubscriptionService();
                var subscriptions = service.List(options);
                if (subscriptions == null)
                {
                    Console.WriteLine("Pas de résultat");
                }
                return Ok(new SubscriptionsResponse
                {
                    Subscriptions = subscriptions,
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        private Subscription GetActiveSubscription(string id)
        {
            var myDem = _dpMyDemenageurUser.GetUserById(id).FirstOrDefault();
            var options = new SubscriptionListOptions
            {
                Customer = myDem.StripeId,
                Status = "active",
            };
            var service = new SubscriptionService();
            var subscriptions = service.List(options);
            if (subscriptions == null)
            {
                Console.WriteLine("Pas de résultat");
            }
            var activeSubId = subscriptions.Data[0].Id;
            var activeSub = service.Get(activeSubId);
            return activeSub;
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

        [HttpPost("unsubscribe/{id:length(24)}")]
        public IActionResult Unsubscribe(string id)
        {
            var service = new SubscriptionService();
            var subscription = GetActiveSubscription(id);

            var options = new SubscriptionUpdateOptions
            {
                CancelAtPeriodEnd = true,
            };
            var updatedSubscription = service.Update(subscription.Id, options);
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

        private void SubscriptionCancelled(string stripeId)
        {
            try
            {
                var user = _usersService.GetByStripeId(stripeId);
                Console.WriteLine("Get user succeeded");
                var data = new MyDemenageurUserRole();
                data.Role = "ServiceProvider";
                data.RoleType = "Basique";
                _usersService.UpdateUserRole(user.Result.Id, data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            const string endpointSecret = "whsec_6b4593329624209d65723ba2da0a09f0c3ca7d104f337739c7d38887a4ae3636";
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    signatureHeader,
                    endpointSecret
                    //this.options.Value.WebhookSecret
                );
                Console.WriteLine($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");

                switch (stripeEvent.Type)
                {
                    case Events.PaymentIntentSucceeded:
                        break;
                    case Events.CustomerSubscriptionDeleted:
                        var cancelledSub = stripeEvent.Data.Object as Subscription;
                        SubscriptionCancelled(cancelledSub.CustomerId);
                        break;
                    case Events.CustomerSubscriptionUpdated:
                        break;
                    default:
                        Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                        break;
                }
                    

                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    Console.WriteLine("A successful payment for {0} was made.", paymentIntent.Amount);
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionDeleted)
                {
                    var cancelledSub = stripeEvent.Data.Object as Subscription;
                    SubscriptionCancelled(cancelledSub.CustomerId);
                }
                else if (stripeEvent.Type == Events.PaymentMethodAttached)
                {
                    var session = stripeEvent.Data.Object as PaymentMethod;
                    Console.WriteLine("Méthode de paiement attachée");
                    // Take some action based on session.
                }
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                Console.WriteLine(e);
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

    }
}
