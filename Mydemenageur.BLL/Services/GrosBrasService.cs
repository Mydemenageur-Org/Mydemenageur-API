using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.BLL.Services.Interfaces;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models.GenericService;
using Mydemenageur.DAL.Models.Users;
using Mydemenageur.BLL.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Mydemenageur.BLL.Services
{
    public class GrosBrasService: IGrosBrasService
    {
        private readonly IDPGrosBras _dpGrosBras;
        private readonly IDPMyDemenageurUser _dpMDUser;

        public GrosBrasService(IDPGrosBras dPGrosBras, IDPMyDemenageurUser dpMDUser)
        {
            _dpGrosBras = dPGrosBras;
            _dpMDUser = dpMDUser;
        }

        public async Task<IList<GrosBras>> GetGrosBras(int pageNumber = -1, int numberOfElementsPerPage = -1)
        {
            var cursor = _dpGrosBras.GetCollection().Find(new BsonDocument());

            if (pageNumber >= 0 && numberOfElementsPerPage > 0)
            {
                cursor.Limit(numberOfElementsPerPage).Skip(pageNumber * numberOfElementsPerPage);
            }
            //IList<GrosBras> grosBras = await _dpGrosBras.Obtain().ToListAsync();

            return await cursor.ToListAsync();
        }

        public async Task<GrosBras> GetGrosBrasById(string id)
        {
            GrosBras grosBrasProfil = await _dpGrosBras.GetGrosBrasById(id).FirstOrDefaultAsync();
            return grosBrasProfil;
        }

        public string CreateGrosBras(string myDemUserId)
        {
            return myDemUserId;
        }

    }
}
