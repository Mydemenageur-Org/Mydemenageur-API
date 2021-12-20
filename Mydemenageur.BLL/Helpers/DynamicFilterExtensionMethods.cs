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
            // Instantiation of a Dict to have key and values of the collection
            var propsCollection = new Dictionary<string, PropertyInfo>(propList.Select(x => new KeyValuePair<string, PropertyInfo>(Char.ToLowerInvariant(x.Name[0]) + x.Name.Substring(1), x)));

            foreach (var param in queryParams)
            {
                if (propsCollection.ContainsKey(param.Key))
                {
                    var prop = propsCollection[param.Key];
                    // this first check will provide a native type verification
                    if (prop.PropertyType.FullName.StartsWith("System") && !prop.IsGenericList())
                    {
                        if (param.Value.Count == 1)
                        {
                            collection = collection.Where(x => prop.GetValue(x, null).ToString() == param.Value.First());
                        }
                    }
                }
                else
                {
                    FilteringWithingNestedProperties(collection, propList, param);
                }
            }

            return collection.ToList();
        }

        public static bool IsGenericList(this object o)
        {
            var oType = o.GetType();
            return (oType.GetTypeInfo().IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)));
        }

        public static Type GetGenericListType(object list)
        {
            if(list == null)
            {
                throw new ArgumentNullException("list");
            }

            var type = list.GetType();

            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(List<>))
                throw new ArgumentException("Type must be List<>, but was " + type.FullName, "list");

            return type.GetGenericArguments()[0];
        }

        private static IMongoQueryable<T> FilteringWithingNestedProperties<T>(IMongoQueryable<T> baseCollection, PropertyInfo[] infos, KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues> param) where T : new()
        {
            Type nestedClassType;
            PropertyInfo[] nestedClassPropList;
            var propList = infos;
            foreach (PropertyInfo prop in propList)
            {
                if(prop.Name == "Id" || (prop.PropertyType.FullName.StartsWith("System") && !prop.PropertyType.IsGenericList())) 
                {
                    continue;
                } else
                {
                    _ = prop.IsGenericList() ? nestedClassType = GetGenericListType(prop.PropertyType) : nestedClassType = prop.PropertyType;
                    // Gettings the different properties of the model T
                    nestedClassPropList = nestedClassType.GetProperties();
                }

                // Instantiation of a Dict to have key and values of the collection
                var propsCollection = new Dictionary<string, PropertyInfo>(nestedClassPropList.Select(x => new KeyValuePair<string, PropertyInfo>(Char.ToLowerInvariant(x.Name[0]) + x.Name.Substring(1), x)));


                if (propsCollection.ContainsKey(param.Key))
                {
                    var nestedClassProp = propsCollection[param.Key];
                    // this first check will provide a native type verification
                    if (nestedClassProp.PropertyType.Name.StartsWith("System") && !nestedClassProp.IsGenericList())
                    {
                        if (param.Value.Count == 1)
                        {
                            baseCollection = baseCollection.Where(x => nestedClassProp.GetValue(x, null).ToString() == param.Value.First());
                        }
                    }
                }
                else
                {
                    FilteringWithingNestedProperties(baseCollection, infos, param);
                }
            }
            return baseCollection;
        }

    }
}
