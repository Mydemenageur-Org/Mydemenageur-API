﻿using System;
using System.Collections.Generic;
using Mydemenageur.API.Models.Services;
using Mydemenageur.API.Entities;
using System.Threading.Tasks;

namespace Mydemenageur.API.Services.Interfaces
{
    public interface ICartonService
    {
        public Task<IList<CartonModel>> GetAllCartons(bool isFlexible, string typeService, string boxNb, string boxSize, string zipCode, string city, Nullable<DateTime> dateDisponibility, Nullable<DateTime> startDisponibility, Nullable<DateTime> endDisponibility, int size);
        public Task<Carton> GetCartonById(string id);
        public Task UpdateCarton(string id, CartonModel carton);
        public Task DeleteService(string id);
        public Task<string> CreateCarton(CartonModel cartons);
    }
}