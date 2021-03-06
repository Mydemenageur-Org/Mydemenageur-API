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

namespace Mydemenageur.IntegrationTests.Tests
{
    public class UserTests : TestBase
    {
        public UserTests(TestApplicationFactory<Startup, FakeStartup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/api/Users/58e36d708a4987491e589c0e", "58e36d708a4987491e589c0e", Roles.Client)]
        public async Task Get_User(string url, string name, string role)
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
        [InlineData("/api/Users/60c08a06a43f7313d8164729", "58e36d708a4987491e589c0e", Roles.Client)]
        public async Task Get_User_Fail(string url, string name, string role)
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
        [InlineData("/api/Users/c02da7e40a2ec30b5e60dd89/pastActions", "58e36d708a4987491e589c0e", Roles.Admin)]
        public async Task Get_PastActions(string url, string name, string role)
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
        [InlineData("/api/Users/60b6064ff2f2711ff6e96e13", "60b6064ff2f2711ff6e96e13", Roles.Client)]
        public async Task Put_User(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var user = new UserUpdateModel
            {
                ProfilePicture = "string",
                FirstName = "Test100",
                LastName = "Test100",
                Gender= "Male",
                Email = "test100@gmail.com",
                Phone = "0707070707",
                Username = "Test100",
                About = "Ceci est un test100",
                Password = "36304548"
            };

            //// Act
            var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.True(true);
        }

        [Theory]
        [InlineData("/api/Users/60c08a18e4a973840ff3600c", "60c08a18e4a973840ff3600c", Roles.Client)]
        public async Task Put_User_Fail(string url, string name, string role)
        {
            dynamic data = new ExpandoObject();
            data.name = name;
            data.role = new[] { role };

            //// Arrange
            var client = Factory.CreateClient();
            client.SetFakeBearerToken((object)data);

            var user = new UserUpdateModel
            {
                ProfilePicture = "string",
                FirstName = "Test100",
                LastName = "Test100",
                Gender = "Male",
                Email = "test100@gmail.com",
                Phone = "0707070707",
                Username = "Test100",
                About = "Ceci est un test100",
                Password = "36304548"
            };

            //// Act
            var response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

            Assert.True(response.StatusCode != HttpStatusCode.OK);
        }
    }
}
