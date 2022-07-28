using System;
using System.Collections.Generic;
using Mydemenageur.DAL.Models.Users;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mydemenageur.DAL.Models;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IGrosBrasService
    {
        public Task<IList<GrosBrasPopulated>> GetGrosBras(QueryString queryString, int pageNumber = -1, int numberOfElementsPerPage = -1, string cityLabel = "");
        public Task<GrosBrasPopulated> GetGrosBrasById(string id);

        public Task<IList<GrosBrasPopulated>> GetRanking(int numberOfGrosBras);
        public Task<string> CreateGrosBras(GrosBras grosBras, string cityName = null);
        public Task<string> UpdateGrosBras(GrosBras grosBras, string cityName = null);

        public Task UploadRealisation(string id, FileModel file);
        public Task<FileModel> GetRealisation(string realisationId);

        public Task<long> CountGrosBras(QueryString queryString);

        public Task CalculatRanking();
    }
}
