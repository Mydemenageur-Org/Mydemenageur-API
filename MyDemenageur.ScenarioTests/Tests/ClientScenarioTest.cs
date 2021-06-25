using Mydemenageur.API;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Clients;
using Mydemenageur.API.Models.Housing;
using Mydemenageur.API.Models.MoveRequest;
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
    public class ClientScenarioTest : TestBase
    {
        public ClientScenarioTest(TestApplicationFactory<Startup, FakeStartup> factory) : base(factory)
        {

        }

        [Fact]
        public async Task ClientScenario()
        {

            var client = Factory.CreateClient();

            ///Register a new User
            var register = new RegisterModel
            {
                ProfilePicture = null,
                FirstName = "Victor",
                LastName = "DENIS",
                Gender = "Female",
                Email = "feldrise@gmail.Com",
                Phone = "0606060606",
                Username = "Feldrise",
                About = "Je suis Feldrise",
                Password = "Feldrise1234",
                Role = "Client"
            };

            var response = await client.PostAsync("/api/Authentication/register", new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json"));

            ///This new user become a client
            ///
            if (!response.StatusCode.ToString().Equals("OK"))
            {
                Assert.True(false, response.Content.ReadAsStringAsync().Result.ToString());
            }

            var clientRegis = new ClientRegisterModel
            {
                UserId = response.Content.ReadAsStringAsync().Result.ToString()
            };

            var responseClientRegister = await client.PostAsync("/api/Clients", new StringContent(JsonConvert.SerializeObject(clientRegis), Encoding.UTF8, "application/json"));

            var moveRequestRegis = new MoveRequestRegisterModel
            {
                UserId = response.Content.ReadAsStringAsync().Result.ToString(),
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

            var responseMoveRequest = await client.PostAsync("/api/MoveRequests", new StringContent(JsonConvert.SerializeObject(moveRequestRegis), Encoding.UTF8, "application/json"));

            if (!responseMoveRequest.StatusCode.ToString().Equals("OK"))
            {
                Assert.True(false, responseMoveRequest.Content.ReadAsStringAsync().Result.ToString());
            }

            var housingRegisStart = new HousingRegisterModel
            {
                HousingType = "Appartement",
                HousingFloor = 36,
                IsElevator = false,
                Surface = 25.3f,
                Address = "85 Rue de l'enceinte",
                Town = "Nantes",
                Zipcode = "44000",
                Country = "France",
                Region = "Pays de la Loire",
                State = "Start",
                MoveRequestId = responseMoveRequest.Content.ReadAsStringAsync().Result.ToString()
            };

            var housingRegisEnd = new HousingRegisterModel
            {
                HousingType = "Appartement",
                HousingFloor = 36,
                IsElevator = false,
                Surface = 25.3f,
                Address = "85 Rue de l'enceinte",
                Town = "Nantes",
                Zipcode = "44000",
                Country = "France",
                Region = "Pays de la Loire",
                State = "Start",
                MoveRequestId = responseMoveRequest.Content.ReadAsStringAsync().Result.ToString()
            };

            var responseHousingStart = await client.PostAsync("/api/Housings", new StringContent(JsonConvert.SerializeObject(housingRegisStart), Encoding.UTF8, "application/json"));
            var responseHousingEnd = await client.PostAsync("/api/Housings", new StringContent(JsonConvert.SerializeObject(housingRegisEnd), Encoding.UTF8, "application/json"));

            if (!responseHousingStart.StatusCode.ToString().Equals("OK"))
            {
                Assert.True(false, responseHousingStart.Content.ReadAsStringAsync().Result.ToString());
            }

            if (!responseHousingEnd.StatusCode.ToString().Equals("OK"))
            {
                Assert.True(false, responseHousingEnd.Content.ReadAsStringAsync().Result.ToString());
            }

            //To login user
            var login = new LoginModel
            {
                Username = "Feldrise",
                Password = "Feldrise1234"
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
                Gender = user.Gender,
                Phone = "0707070707",
                Username = user.Username,
                About = user.About
            };

            client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", user.Token);

            var responseMoverUpdate = await client.PutAsync("/api/Users/"+user.Id, new StringContent(JsonConvert.SerializeObject(updateUser), Encoding.UTF8, "application/json"));

            if (!responseMoverUpdate.StatusCode.ToString().Equals("OK"))
            {
                Assert.True(false, responseMoverUpdate.Content.ReadAsStringAsync().Result.ToString());
            }
        }
    }
}
