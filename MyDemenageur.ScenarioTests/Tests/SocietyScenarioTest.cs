using Mydemenageur.API;
using Mydemenageur.API.Entities;
using Mydemenageur.API.Models.Clients;
using Mydemenageur.API.Models.Movers;
using Mydemenageur.API.Models.Society;
using Mydemenageur.API.Models.Users;
using Mydemenageur.API.Models.Vehicule;
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
    public class SocietyScenarioTest : TestBase
    {
        public SocietyScenarioTest(TestApplicationFactory<Startup, FakeStartup> factory) : base(factory)
        {

        }

        [Fact]
        public async Task SocietyScenario()
        {

            var client = Factory.CreateClient();

            ///Register a new User
            var register = new RegisterModel
            {
                ProfilePicture = null,
                FirstName = "Fabien",
                LastName = "TRIK",
                Email = "fabientrick@gmail.Com",
                Phone = "0606060606",
                Username = "Frick",
                About = "Je suis Frick",
                Password = "Frick1234",
                Role = "Society"
            };

            var response = await client.PostAsync("/api/Authentication/register", new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json"));

            //This new user become a client
            if (!response.StatusCode.ToString().Equals("OK"))
            {
                Assert.True(false, response.Content.ReadAsStringAsync().Result.ToString());
            }

            var societyRegis = new SocietyRegisterModel
            {
                SocietyName = "TestSocietyScenario",
                ManagerId = response.Content.ReadAsStringAsync().Result.ToString(),
                EmployeeNumber = 666,
                Address = "85 Rue de l'enceinte",
                Town = "Nantes",
                Zipcode = "44000",
                Country = "France",
                Region = "Pays de la Loire"
            };

            var responseSocietyRegis = await client.PostAsync("/api/Societies", new StringContent(JsonConvert.SerializeObject(societyRegis), Encoding.UTF8, "application/json"));

            if (!responseSocietyRegis.StatusCode.ToString().Equals("OK"))
            {
                Assert.True(false, responseSocietyRegis.Content.ReadAsStringAsync().Result.ToString());
            }
            //Register new vehicle
            var vehicleRegis = new VehiclesRegisterModel
            {
                SocietyId = responseSocietyRegis.Content.ReadAsStringAsync().Result.ToString(),
                VehiclesNumber = 5,
                HasTarpaulinVehicule = true,
                PTAC_TarpaulinVehicule = 1250,
                HasHardWallVehicule = false,
                PTAC_HardWallVehicule = 0,
                CanTransportHorse = false,
                CanTransportVehicule = true,
                TotalCapacity = 6250
            };

            var responseVehicleRegis = await client.PostAsync("/api/vehicles", new StringContent(JsonConvert.SerializeObject(vehicleRegis), Encoding.UTF8, "application/json"));

            if (!responseVehicleRegis.StatusCode.ToString().Equals("OK"))
            {
                Assert.True(false, responseVehicleRegis.Content.ReadAsStringAsync().Result.ToString());
            }
            //To login the new user
            var login = new LoginModel
            {
                Username = "Frick",
                Password = "Frick1234"
            };

            var responseUserLogin = await client.PostAsync("/api/Authentication/login", new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json"));
            var userJSONString = await responseUserLogin.Content.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(userJSONString);

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
