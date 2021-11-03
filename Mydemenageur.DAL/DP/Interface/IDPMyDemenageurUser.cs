﻿using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Mydemenageur.DAL.Models.Users;
using System;
using System.Linq.Expressions;

namespace Mydemenageur.DAL.DP.Interface
{
    public interface IDPMyDemenageurUser
    {
        public IMongoQueryable<MyDemenageurUser> Obtain();

        public IMongoCollection<MyDemenageurUser> GetCollection();

        public IMongoQueryable<MyDemenageurUser> GetUserById(string idUser);
        public IMongoQueryable<MyDemenageurUser> GetFiltered(Expression<Func<MyDemenageurUser, bool>> predicate);
    }
}