using Mydemenageur.DAL.Models.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IGenericServicesService
    {
        Task<GenericService> GetGenericService(string id);
        Task<IList<GenericService>> GetGenericServices(string name);

        Task<string> CreateGenericService(GenericService toCreate);

        Task UpdateGenericService(GenericService toUpdate);
    }
}
