using System;
using System.Collections.Generic;
using Mydemenageur.DAL.Models;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface ICitiesService
    {
        public Task<IList<City>> GetAllCities();
        public Task<City> CreateNewCity(string label);
    }
}
