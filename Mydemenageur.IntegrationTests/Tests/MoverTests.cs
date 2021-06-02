using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using Mydemenageur.API;
using Mydemenageur.IntegrationTests.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Mydemenageur.API.Entities;
using System.Dynamic;
using System.Net;
using Newtonsoft.Json;
using Mydemenageur.API.Models.Clients;
using Mydemenageur.API.Models.Vehicule;
using Mydemenageur.API.Models.Movers;

namespace Mydemenageur.IntegrationTests.Tests
{
    public class MoverTests : TestBase
    {
        public MoverTests(TestApplicationFactory<Startup, FakeStartup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/api/Movers/c6b9e1ee60530ec4bc82d701")]
        public async Task Get_Mover(string url)
        {
            dynamic data = new ExpandoObject();
            data.name = "c6b9e1ee60530ec4bc82d701";
            data.role = new[] { Roles.Client };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            //// Act
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("/api/Movers")]
        public async Task Post_Vehicule(string url)
        {
            dynamic data = new ExpandoObject();
            data.name = "8ff13858c921c857cfa53401";
            data.role = new[] { Roles.Client };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var MoverRegis = new MoverRegisterModel
            {
                UserId = "60b751e920d55070861a34a2",
                FileId = null
            };

            //// Act
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(MoverRegis), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("/api/Movers/c6b9e1ee60530ec4bc82d701")]
        public async Task Put_Vehicule(string url)
        {
            dynamic data = new ExpandoObject();
            data.name = "ead29c8d187a26eaf3b39885";
            data.role = new[] { Roles.Client };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var MoverUpd = new MoverUpdateModel
            {
                IsVIP = false,
                AverageCustomer = 6.6f
            };

            //// Act
            var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(MoverUpd), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }
    }
}
