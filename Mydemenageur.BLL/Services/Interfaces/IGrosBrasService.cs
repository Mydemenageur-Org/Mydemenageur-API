using System;
using System.Collections.Generic;
using Mydemenageur.DAL.Models.Users;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IGrosBrasService
    {
        public Task<IList<GrosBrasPopulated>> GetGrosBras(int pageNumber = -1, int numberOfElementsPerPage = -1);
        public Task<GrosBrasPopulated> GetGrosBrasById(string id);
        public string CreateGrosBras(string myDemUserId);
        public long CountGrosBras();
    }
}
