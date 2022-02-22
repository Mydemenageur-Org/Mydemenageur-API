using System;
using System.Collections.Generic;
using Mydemenageur.DAL.Models.Users;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IGrosBrasService
    {
        public Task<IList<GrosBrasPopulated>> GetGrosBras(IQueryCollection queryParams, int pageNumber = -1, int numberOfElementsPerPage = -1, string cityLabel = "");
        public Task<GrosBrasPopulated> GetGrosBrasById(string id);

        public Task<IList<GrosBrasPopulated>> GetRanking(int numberOfGrosBras);
        public Task<string> CreateGrosBras(string cityName, GrosBras grosBras);
        public long CountGrosBras();
    }
}
