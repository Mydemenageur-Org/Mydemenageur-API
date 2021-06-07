using Mydemenageur.API;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Clients;
using Mydemenageur.API.Models.Movers;
using Mydemenageur.API.Models.Society;
using Mydemenageur.API.Models.Users;
using Mydemenageur.ScenarioTests.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mydemenageur.ScenarioTests.Tests
{
    public class MoverScenarioTest : TestBase
    {
        public MoverScenarioTest(TestApplicationFactory<Startup, FakeStartup> factory) : base(factory)
        {

        }

        [Fact]
        public async Task MoverScenario()
        {

            var client = Factory.CreateClient();

            ///Register a new User
            var register = new RegisterModel
            {
                ProfilePicture = null,
                FirstName = "Julien",
                LastName = "TRIK",
                Email = "julientrick@gmail.Com",
                Phone = "0606060606",
                Username = "Jrick",
                About = "Je suis Jrick",
                Password = "Jrick1234",
                Role = "Mover"
            };

            var response = await client.PostAsync("/api/Authentication/register", new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json"));

            ///This new user become a client
            ///
            if (!response.StatusCode.ToString().Equals("OK"))
            {
                Assert.True(false, response.Content.ReadAsStringAsync().Result.ToString());
            }

            var clientRegis = new MoverRegisterModel
            {
                UserId = response.Content.ReadAsStringAsync().Result.ToString(),
                FileId = new List<string>()
            };

            var responseUserRegister = await client.PostAsync("/api/Movers", new StringContent(JsonConvert.SerializeObject(clientRegis), Encoding.UTF8, "application/json"));

            if (!responseUserRegister.StatusCode.ToString().Equals("OK"))
            {
                Assert.True(false, responseUserRegister.Content.ReadAsStringAsync().Result.ToString());
            }

            ///To login the new user
            if (!responseUserRegister.StatusCode.ToString().Equals("OK"))
            {
                Assert.True(false, responseUserRegister.Content.ReadAsStringAsync().Result.ToString());
            }

            var login = new LoginModel
            {
                Username = "Jrick",
                Password = "Jrick1234"
            };

            var responseUserLogin = await client.PostAsync("/api/Authentication/login", new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json"));
            var userJSONString = await responseUserLogin.Content.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(userJSONString);

            if (!responseUserLogin.StatusCode.ToString().Equals("OK"))
            {
                Assert.True(false, responseUserLogin.Content.ReadAsStringAsync().Result.ToString());
            }

            var updateUser = new UserUpdateModel
            {
                ProfilePicture = user.ProfilePicture,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = "0707070707",
                Username = user.Username,
                About = user.About
            };

            client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", user.Token);

            var responseMoverUpdate = await client.PutAsync("/api/Users/" + user.Id, new StringContent(JsonConvert.SerializeObject(updateUser), Encoding.UTF8, "application/json"));

            if (!responseMoverUpdate.StatusCode.ToString().Equals("OK"))
            {
                Assert.True(false, responseMoverUpdate.Content.ReadAsStringAsync().Result.ToString());
            }
        }
    }
}
