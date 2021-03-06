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
        [InlineData("/api/Movers/c6b9e1ee60530ec4bc82d701", "c6b9e1ee60530ec4bc82d701", Roles.Client)]
        public async Task Get_Mover(string url, string name, string role)
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
        [InlineData("/api/Movers/60c088baca53c985efbb1ab1", "c6b9e1ee60530ec4bc82d701", Roles.Client)]
        public async Task Get_Mover_Fail(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            //// Act
            var response = await client.GetAsync(url);

            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("/api/Movers", "8ff13858c921c857cfa53401", Roles.Client)]
        public async Task Post_Mover(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

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
        [InlineData("/api/Movers", "c6b9e1ee60530ec4bc82d701", Roles.Client)]
        public async Task Post_Mover_Fail(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var MoverRegis = new MoverRegisterModel
            {
                UserId = "60c089005c454fac81e3846b",
                FileId = null
            };

            //// Act
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(MoverRegis), Encoding.UTF8, "application/json"));

            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("/api/Movers/c6b9e1ee60530ec4bc82d701", "ead29c8d187a26eaf3b39885", Roles.Client)]
        public async Task Put_Mover(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

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

        [Theory]
        [InlineData("/api/Movers/60c089155f4e2a4c3bb25819", "ead29c8d187a26eaf3b39885", Roles.Client)]
        public async Task Put_Mover_Fail(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

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

            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("/api/Movers/736216dab52a7dff811ab853", "addc792a46a7f43619201a5b", Roles.Admin)]
        public async Task X_Delete_Mover(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            //// Act
            var response = await client.DeleteAsync(url);

            Assert.True(true);
        }

        [Theory]
        [InlineData("/api/Movers/60c36560ba08a89566e390d1", "addc792a46a7f43619201a5b", Roles.Admin)]
        [InlineData("/api/Movers/736216dab52a7dff811ab853", "addc792a46a7f43619201a5b", Roles.Client)]
        [InlineData("/api/Movers/736216dab52a7dff811ab853", "addc792a46a7f43619201a5b", Roles.Mover)]
        public async Task X_Delete_Mover_Fail(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            //// Act
            var response = await client.DeleteAsync(url);

            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }
    }
}
