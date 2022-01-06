using System;
using System.Collections.Generic;
using Mydemenageur.DAL.Models.Users;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IGrosBrasService
    {
        public Task<IList<GrosBras>> GetGrosBras();
        public Task<GrosBras> GetGrosBrasById(string id);
        public string CreateGrosBras(string myDemUserId);
    }
}
