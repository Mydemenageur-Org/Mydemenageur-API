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
using Mydemenageur.API.Models.Users;
using Mydemenageur.API.Models.Society;
using Mydemenageur.API.Models.MoveRequest;

namespace Mydemenageur.IntegrationTests.Tests
{
    public class MoveRequestTests : TestBase
    {
        public MoveRequestTests(TestApplicationFactory<Startup, FakeStartup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/api/MoveRequests/d39ac2a9e907daa5dd1bc23b", "addc792a46a7f43619201a5b", Roles.Client)]
        public async Task Get_MoveRequest(string url, string name, string role)
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
        [InlineData("/api/MoveRequests/60c087d7397befc7bc2c6870", "addc792a46a7f43619201a5b", Roles.Client)]
        public async Task Get_MoveRequest_Fail(string url, string name, string role)
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
        [InlineData("/api/MoveRequests", "60b6064ff2f2711ff6e96e13", Roles.Client)]
        public async Task Post_MoveRequest(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var moveRequestRegis = new MoveRequestRegisterModel
            {
                UserId = "addc792a46a7f43619201a5b",
                Title = "Test démé test",
                MoveRequestVolume = 35,
                NeedFurnitures = false,
                NeedAssembly = false,
                NeedDiassembly = true,
                MinimumRequestDate = new DateTime(),
                MaximumRequestDate = new DateTime(),
                HeavyFurnitures = new List<string>(),
                AdditionalInformation = "Non"
            };

            //// Act
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(moveRequestRegis), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("/api/MoveRequests", "60b6064ff2f2711ff6e96e13", Roles.Client)]
        public async Task Post_MoveRequest_Fail(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var moveRequestRegis = new MoveRequestRegisterModel
            {
                UserId = "60c0880f1e9a15c0cbf8f291",
                Title = "Test démé test",
                MoveRequestVolume = 35,
                NeedFurnitures = false,
                NeedAssembly = false,
                NeedDiassembly = true,
                MinimumRequestDate = new DateTime(),
                MaximumRequestDate = new DateTime(),
                HeavyFurnitures = new List<string>(),
                AdditionalInformation = "Non"
            };

            //// Act
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(moveRequestRegis), Encoding.UTF8, "application/json"));

            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("/api/MoveRequests/ee540bb5a8acc64a029b28b7", "58e36d708a4987491e589c0e", Roles.Client)]
        public async Task Put_MoveRequest(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var moveRequestUpd = new MoveRequestUpdateModel
            {
                UserId = "58e36d708a4987491e589c0e",
                Title = "Test démé test",
                MoveRequestVolume = 35,
                NeedFurnitures = false,
                NeedAssembly = false,
                NeedDiassembly = true,
                MinimumRequestDate = new DateTime(),
                MaximumRequestDate = new DateTime(),
                HeavyFurnitures = new List<string>(),
                AdditionalInformation = "Non"
            };

            //// Act
            var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(moveRequestUpd), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("/api/MoveRequests/ee540bb5a8acc64a029b28b7", "58e36d708a4987491e589c0e", Roles.Client)]
        public async Task Put_MoveRequest_Fail(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var moveRequestUpd = new MoveRequestUpdateModel
            {
                UserId = "60c1e2fc333e72f2590073bb",
                Title = "Test démé test",
                MoveRequestVolume = 35,
                NeedFurnitures = false,
                NeedAssembly = false,
                NeedDiassembly = true,
                MinimumRequestDate = new DateTime(),
                MaximumRequestDate = new DateTime(),
                HeavyFurnitures = new List<string>(),
                AdditionalInformation = "Non"
            };

            //// Act
            var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(moveRequestUpd), Encoding.UTF8, "application/json"));

            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }

    }
}
