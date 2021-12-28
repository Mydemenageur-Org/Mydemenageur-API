﻿using Microsoft.AspNetCore.Http;
using Mydemenageur.DAL.Models.GenericService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Services.Interfaces
{
    public interface IGenericServicesService
    {
        Task<GenericServicePopulated> GetGenericService(string id, IList<string> fields);
        Task<GenericService> GetBaseGenericService(string name);
        Task<List<GenericService>> GetGenericServices(IQueryCollection queryParams);

        Task<GenericService> CreateGenericService(GenericService toCreate);

        Task UpdateGenericService(GenericService toUpdate);
    }
}
