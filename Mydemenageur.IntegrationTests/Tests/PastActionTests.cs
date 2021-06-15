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
using Mydemenageur.API.Models.PastAction;

namespace Mydemenageur.IntegrationTests.Tests
{
    public class PastActionTests : TestBase
    {
        public PastActionTests(TestApplicationFactory<Startup, FakeStartup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/api/PastActions/60c86eb9745cd564a5bb3f79", "c02da7e40a2ec30b5e60dd89", Roles.Admin)]
        public async Task Get_PastAction(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

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
        [InlineData("/api/PastActions/60c86eb9745cd564a5bb3f79", "c02da7e40a2ec30b5e60dd89", Roles.Client)]
        public async Task Get_PastAction_Fail(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            //// Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("/api/PastActions", "60b6064ff2f2711ff6e96e13", Roles.Client)]
        public async Task Post_Client(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var pastActionRegis = new PastActionRegisterModel
            {
                ActionIcon = "60c8721c63dd43b95ae4d7a3",
                Title = "Contact courrier",
                Description = "Contact de courrier demandé",
                UserId = "addc792a46a7f43619201a5b"
            };

            //// Act
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(pastActionRegis), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("/api/PastActions", "60b6064ff2f2711ff6e96e13", Roles.Client)]
        public async Task Post_Client_Fail(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var pastActionRegis = new PastActionRegisterModel
            {
                ActionIcon = "60c8721c63dd43b95ae4d7a3",
                Title = "Contact courrier",
                Description = "Contact de courrier demandé",
                UserId = "addc792a87a7f43619201a5b"
            };

            //// Act
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(pastActionRegis), Encoding.UTF8, "application/json"));

            // Assert
            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }



    }
}
