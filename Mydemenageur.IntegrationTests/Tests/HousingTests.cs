﻿using Microsoft.AspNetCore.Hosting;
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
using Mydemenageur.API.Models.Housing;

namespace Mydemenageur.IntegrationTests.Tests
{
    public class HousingTests : TestBase
    {
        public HousingTests(TestApplicationFactory<Startup, FakeStartup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/api/Housings/9f30c23e20c027855197bfef")]
        public async Task Get_Housing(string url)
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
        [InlineData("/api/Housings")]
        public async Task Post_Housing(string url)
        {
            dynamic data = new ExpandoObject();
            data.name = "c02da7e40a2ec30b5e60dd89";
            data.role = new[] { Roles.Client };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var housingRegis = new HousingRegisterModel
            {
                HousingType = "Appartement",
                HousingFloor = 36,
                IsElevator = false,
                Surface = 25.3f,
                Adress = "85 Rue de l'enceinte",
                Town = "Nantes",
                Zipcode = "44000",
                Country = "France",
                Region = "Pays de la Loire",
                State = "Start",
                MoveRequestId = "412f25549488c88751e09e99"
            };

            //// Act
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(housingRegis), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("/api/Housings/11eec27e7df06675383d1617")]
        public async Task Put_Housing(string url)
        {
            dynamic data = new ExpandoObject();
            data.name = "58e36d708a4987491e589c0e";
            data.role = new[] { Roles.Client };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var housingUpd = new HousingUpdateModel
            {
                HousingType = "Appartement",
                HousingFloor = 36,
                IsElevator = false,
                Surface = 25.3f,
                Adress = "85 Rue de l'enceinte",
                Town = "Nantes",
                Zipcode = "44000",
                Country = "France",
                Region = "Pays de la Loire",
                State = "Start",
                MoveRequestId = "412f25549488c88751e09e99"
            };

            //// Act
            var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(housingUpd), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

    }
}
