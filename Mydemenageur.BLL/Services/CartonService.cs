using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Entities;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Mydemenageur.DAL.DP.Interface;
using Mydemenageur.DAL.Models.Services;
using Mydemenageur.BLL.Extensions;
using MongoDB.Driver;
using Mydemenageur.BLL.Services.Interfaces;
using System.Linq.Expressions;

namespace Mydemenageur.BLL.Services
{
    public class CartonService: ICartonService
    {
        public IDPCartons _dpCartons;

        public CartonService(IDPCartons dPCartons)
        {
            _dpCartons = dPCartons;
        }

        public async Task<IList<CartonModel>> GetAllCartons(bool isFlexible, string typeService, string boxNb, string boxSize, string zipCode, string city,Nullable<DateTime> dateDisponibility, Nullable<DateTime> startDisponibility, Nullable<DateTime> endDisponibility, int size )
        {
            int iterator = 0;
            List<Carton> cartonsEntities = new List<Carton>();
            List<CartonModel> cartonsModels = new List<CartonModel>();
            List<string> paramsFilter = new List<string>() { typeService, boxNb, boxSize, zipCode, city };
            List<Expression<Func<Carton, bool>>> queries = new List<Expression<Func< Carton, bool>>>();

            queries.Add(w => w.ResearchType == paramsFilter[0]);
            queries.Add(w => w.BoxNumber == paramsFilter[1]);
            queries.Add(w => w.BoxSize == paramsFilter[2]);
            queries.Add(w => w.ZipCode == paramsFilter[3]);
            queries.Add(w => w.City == paramsFilter[4]);

            IMongoQueryable<Carton> _cartons = _dpCartons.Obtain();

            foreach (string filterLabel in paramsFilter)
            {
                string tmp = filterLabel;
                if (tmp != null)
                {
                    _cartons = _cartons.Where(queries[iterator]);
                }
                ++iterator;
            }

            if(dateDisponibility != null)
            {
                _cartons = _cartons.Where(w => w.DateDisponibility == dateDisponibility);
            }

            if(startDisponibility != null)
            {
                _cartons = _cartons.Where(w => w.StartDisponibility == startDisponibility);
            }

            if(endDisponibility != null)
            {
                _cartons = _cartons.Where(w => w.EndDisponibility == endDisponibility);
            }

            _cartons = _cartons.Where(w => w.isFlexible == isFlexible);

            if (size != 0)
            {
                _cartons.Take(size);
            }

            cartonsEntities = await _cartons.ToListAsync();

            foreach(Carton carton in cartonsEntities)
            {
                CartonModel buff = carton.ToModelCarton();
                cartonsModels.Add(buff);
            }

            return cartonsModels;
        }

        public async Task<Carton> GetCartonById(string id)
        {
            Carton carton = await _dpCartons.GetCartonById(id).FirstOrDefaultAsync();
            if (carton == null) throw new Exception("The carton service does not exist");
            return carton;
        }

        public async Task UpdateCarton(string id, CartonModel carton)
        {
            Carton _carton = await _dpCartons.GetCartonById(id).FirstOrDefaultAsync();
            if (_carton == null) throw new Exception("The carton service does not exist");
            if (_carton.UserId != carton.UserId) throw new Exception("You are not allowed to update this service");
            var update = Builders<Carton>.Update
                .Set(db => db.ResearchType, carton.ResearchType)
                .Set(db => db.BoxNumber, carton.BoxNumber)
                .Set(db => db.BoxSize, carton.BoxSize)
                .Set(db => db.DateDisponibility, carton.DateDisponibility)
                .Set(db => db.isFlexible, carton.isFlexible)
                .Set(db => db.StartDisponibility, carton.StartDisponibility)
                .Set(db => db.EndDisponibility, carton.EndDisponibility)
                .Set(db => db.ZipCode, carton.ZipCode)
                .Set(db => db.City, carton.City)
                .Set(db => db.AnnounceTitle, carton.AnnounceTitle)
                .Set(db => db.ModifiedAt, DateTime.Now);

            await _dpCartons.GetCollection().UpdateOneAsync(db =>
                db.Id == id,
                update
            );
        }

        public async Task DeleteService(string id)
        {
            if(id != null)
            {
                Carton cartonToBeDeleted = await _dpCartons.GetCartonById(id).FirstOrDefaultAsync();
                if (cartonToBeDeleted == null) throw new Exception("The service you are requesting does not exist");

                await _dpCartons.GetCollection().DeleteOneAsync(db => db.Id == id);
            }
        }

        public async Task<string> CreateCarton(CartonModel cartons )
        {
            Carton carton = new Carton()
            {
                ResearchType = cartons.ResearchType,
                BoxNumber = cartons.BoxNumber,
                BoxSize = cartons.BoxSize,
                DateDisponibility = cartons.DateDisponibility,
                isFlexible = cartons.isFlexible,
                StartDisponibility = cartons.StartDisponibility,
                EndDisponibility = cartons.EndDisponibility,
                ZipCode = cartons.ZipCode,
                City = cartons.City,
                AnnounceTitle = cartons.AnnounceTitle,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                UserId = cartons.UserId
            };

            await _dpCartons.GetCollection().InsertOneAsync(carton);

            return carton.Id;
        }
    }
}
