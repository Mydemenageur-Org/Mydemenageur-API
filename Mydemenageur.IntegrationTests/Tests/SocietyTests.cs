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

namespace Mydemenageur.IntegrationTests.Tests
{
    public class SocietyTests : TestBase
    {
        public SocietyTests(TestApplicationFactory<Startup, FakeStartup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/api/Societies/59daf9980effbd5bea0cd89a")]
        public async Task Get_Society(string url)
        {
            dynamic data = new ExpandoObject();
            data.name = "c02da7e40a2ec30b5e60dd89";
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
        [InlineData("/api/Societies")]
        public async Task Post_Society(string url)
        {
            dynamic data = new ExpandoObject();
            data.name = "60b6064ff2f2711ff6e96e13";
            data.role = new[] { Roles.Client };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var societyRegis = new SocietyRegisterModel
            {
                SocietyName = "TestSociety15",
                ManagerId = "8ff13858c921c857cfa53401",
                EmployeeNumber = 666,
                Address = "85 Rue de l'enceinte",
                Town = "Nantes",
                Zipcode = "44000",
                Country = "France",
                Region = "Pays de la Loire"
            };

            //// Act
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(societyRegis), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("/api/Societies/59daf9980effbd5bea0cd89a")]
        public async Task Put_Society(string url)
        {
            dynamic data = new ExpandoObject();
            data.name = "8ff13858c921c857cfa53401";
            data.role = new[] { Roles.Client };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var SocietyUpd = new SocietyUpdateModel
            {
                SocietyName = "Gyslaine & co",
                ManagerId = "de2b15c7b3f97f6bdd0d8bde",
                EmployeeNumber = 78,
                Address = "93 Rue du vélo d'appartement",
                Town = "Lille",
                Zipcode = "59000",
                Country = "France",
                Region = "Hauts-de-France"
            };

            //// Act
            var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(SocietyUpd), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

    }
}
