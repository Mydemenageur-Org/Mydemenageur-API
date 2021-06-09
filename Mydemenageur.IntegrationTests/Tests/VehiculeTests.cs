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
using Mydemenageur.API.Models.Vehicule;

namespace Mydemenageur.IntegrationTests.Tests
{
    public class VehiculeTests : TestBase
    {
        public VehiculeTests(TestApplicationFactory<Startup, FakeStartup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/api/Vehicles/728387adfe9be99566803885")]
        public async Task Get_Vehicule(string url)
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
        [InlineData("/api/Vehicles/60c08a8f0f241271fc022bc7")]
        public async Task Get_Vehicule_Fail(string url)
        {
            dynamic data = new ExpandoObject();
            data.name = "c6b9e1ee60530ec4bc82d701";
            data.role = new[] { Roles.Client };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            //// Act
            var response = await client.GetAsync(url);

            if (!response.StatusCode.ToString().Equals("400"))
            {
                Assert.True(true);
            }
            else
            {
                Assert.True(false);
            }
        }

        [Theory]
        [InlineData("/api/Vehicles")]
        public async Task Post_Vehicule(string url)
        {
            dynamic data = new ExpandoObject();
            data.name = "ead29c8d187a26eaf3b39885";
            data.role = new[] { Roles.Client };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var VehiculeRegis = new VehiclesRegisterModel
            {
                SocietyId = "507f1f77bcf86cd799439011",
                VehiclesNumber = 5,
                HasTarpaulinVehicule = true,
                PTAC_TarpaulinVehicule = 750,
                HasHardWallVehicule = true,
                PTAC_HardWallVehicule = 4500,
                CanTransportHorse = true,
                CanTransportVehicule = false,
                TotalCapacity = 5250
            };

            //// Act
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(VehiculeRegis), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("/api/Vehicles")]
        public async Task Post_Vehicule_Fail(string url)
        {
            dynamic data = new ExpandoObject();
            data.name = "8ff13858c921c857cfa53401";
            data.role = new[] { Roles.Client };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var VehiculeRegis = new VehiclesRegisterModel
            {
                SocietyId = "60c08af8c0e46c3b0403b62d",
                VehiclesNumber = 5,
                HasTarpaulinVehicule = true,
                PTAC_TarpaulinVehicule = 750,
                HasHardWallVehicule = true,
                PTAC_HardWallVehicule = 4500,
                CanTransportHorse = true,
                CanTransportVehicule = false,
                TotalCapacity = 5250
            };

            //// Act
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(VehiculeRegis), Encoding.UTF8, "application/json"));

            if (!response.StatusCode.ToString().Equals("400"))
            {
                Assert.True(true);
            }
            else
            {
                Assert.True(false);
            }
        }

        [Theory]
        [InlineData("/api/Vehicles/728387adfe9be99566803885")]
        public async Task Put_Vehicule(string url)
        {
            dynamic data = new ExpandoObject();
            data.name = "c6b9e1ee60530ec4bc82d701";
            data.role = new[] { Roles.Client };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var VehiculeUpd = new VehiclesUpdateModel
            {
                VehiclesNumber = 5,
                HasTarpaulinVehicule = true,
                PTAC_TarpaulinVehicule = 750,
                HasHardWallVehicule = true,
                PTAC_HardWallVehicule = 4500,
                CanTransportHorse = true,
                CanTransportVehicule = false,
                TotalCapacity = 5250
            };

            //// Act
            var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(VehiculeUpd), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("/api/Vehicles/60c08bd907f3551cb0865fb4")]
        public async Task Put_Vehicule_Fail(string url)
        {
            dynamic data = new ExpandoObject();
            data.name = "c6b9e1ee60530ec4bc82d701";
            data.role = new[] { Roles.Client };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var VehiculeUpd = new VehiclesUpdateModel
            {
                VehiclesNumber = 5,
                HasTarpaulinVehicule = true,
                PTAC_TarpaulinVehicule = 750,
                HasHardWallVehicule = true,
                PTAC_HardWallVehicule = 4500,
                CanTransportHorse = true,
                CanTransportVehicule = false,
                TotalCapacity = 5250
            };

            //// Act
            var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(VehiculeUpd), Encoding.UTF8, "application/json"));

            if (!response.StatusCode.ToString().Equals("400"))
            {
                Assert.True(true);
            }
            else
            {
                Assert.True(false);
            }
        }
    }
}
