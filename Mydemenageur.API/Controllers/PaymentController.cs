using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
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
            // return json: publishableKey (.env)
            return new ConfigResponse
            {
                PublishableKey = this.options.Value.StripePublicKey,
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

    }
}
