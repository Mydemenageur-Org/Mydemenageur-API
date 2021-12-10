using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mydemenageur.BLL.Helpers
{
    public static class DynamicFilterExtensionMethods
    {
        public static List<T> FilterByQueryParamsMongo<T>(this IMongoQueryable<T> collection, IQueryCollection queryParams) where T : new()
        {
            var aggregate = new List<T>();
            // Getting the current type of T to get then properties
            var classType = typeof(T);
            // Gettings the different properties of the model T
            var propList = classType.GetProperties();
            // Instantiation of a Dict to have key and values
            var props = new Dictionary<string, PropertyInfo>(propList.Select(x => new KeyValuePair<string, PropertyInfo>(Char.ToLowerInvariant(x.Name[0]) + x.Name.Substring(1), x)));

            foreach (var param in queryParams)
            {
                if (props.ContainsKey(param.Key))
                {
                    var prop = props[param.Key];
                    if (param.Value.Count == 1)
                    {
                        collection = collection.Where(x => prop.GetValue(x, null).ToString() == param.Value.First());
                    }
                }
            }

            return collection.ToList();
        }
    }
}
