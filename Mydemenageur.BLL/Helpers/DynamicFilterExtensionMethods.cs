﻿using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Mydemenageur.BLL.Helpers
{
    public static class DynamicFilterExtensionMethods
    {
        /// <summary>
        /// This function allows to get data from a MongoDB collection with advanced filters
        /// 
        /// The filters allows to search with classical filters and metadata filters.
        ///
        /// > A "metadata filter" is a filter to search through list with key -> value pairs
        /// > You have an exemple below.
        /// 
        /// To use the metadata system, a few fields are requiered :
        ///  - "metadata-key-number" : the number of metadata keys we have to search
        ///  - "metadata-{i}-key" : the metadata key name where {i} is the number of 
        ///  the metadata key
        /// In order to search through metadata, we use the syntaxe "metadata{i}.value" where 
        /// {i} is the number of the metdata key, and value is the value of the key
        /// 
        /// For nested elements, we simple add a dot (".") to create the path through the desired fields/metadata 
        /// and for non metadata elements it workds by key/value
        /// 
        /// Here is an exemple of collection with classical fields, and a metadata array :
        /// [
        ///     {
        ///         "owner": "Léo",
        ///         "books-infos: [
        ///             {
        ///                 "key": "title",
        ///                 "value": "Picsou Magasine"
        ///             },
        ///             {
        ///                 "key": "page-number",
        ///                 "value": 128
        ///             }
        ///         ]
        ///     },
        ///     {
        ///         "owner": "Victor",
        ///         "books-info": [
        ///             {
        ///                 "key": "title",
        ///                 "value": "Le Seigneur des anneaux"
        ///             },
        ///             {
        ///                 "key": "page-number"
        ///                 "value": "1280"
        ///             }
        ///         ]
        ///     },
        ///     ...
        /// ]
        /// 
        /// For the exemple, if we want to filter books names "Le Seigneur des anneaux"
        /// we would need 
        ///  - "metadata-key-number" = 1
        ///  - "matadata-1-key" = "key"
        ///  - "books-info.metadata1.title" = "Le Seigneur des anneaux" 
        ///  in the end, in the request it will transform in something like 
        ///  {"books-info.key" = "title" & "books-info.value" = "Le Seigneur des anneaux"}.
        /// 
        /// If we simply wanted the nooks owned by Victor we would just have need "owner" = "Victor"
        ///
        /// In an URL, the two request would look like : 
        ///  - https://host.ext/path?metadata-key-number=1&metadata-1-key=key&books-info.metadata1.title=Le%20Seigneur%20des%20anneaux
        ///  - https://host.ext/path?owner=Victor
        /// You can of course combine both and use an URL like https://host.ext/path?metadata-key-number=1&metadata-1-key=key&books-info.metadata1.title=Le%20Seigneur%20des%20anneaux 
        /// wich will filter books owned by Victor and whose title is "Le Seigneur des anneaux".
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async static Task<List<T>> FilterByQueryParamsMongo<T>(this IMongoCollection<T> collection, IQueryCollection queryParams) where T : new()
        {
            string metadataKeysNumberKey = "metadata-keys-number";
            List<FilterDefinition<T>> allFilters = new();

            // First part, we parse all query with metadata
            if (queryParams.ContainsKey(metadataKeysNumberKey))
            {
                // NOTE : this might crash, so the caller should handle exception
                int metadataKeysNumber = int.Parse(queryParams[metadataKeysNumberKey]);

                for (int i = 1; i <= metadataKeysNumber; ++i)
                {
                    string metadataKey = queryParams[$"metadata{i}-key"];

                    foreach (string key in queryParams.Keys)
                    {
                        if (key.Contains($"metadata{i}."))
                        {
                            List<string> keyElements = key.Split('.').ToList();

                            // The metadata may be nested so we want to make sure we don't loose that
                            List<string> keyNestedElements = new(keyElements);
                            // We need to remove the two last items of the nested elements 
                            // beceause they are only informatives
                            keyNestedElements.RemoveAt(keyNestedElements.Count - 1);
                            keyNestedElements.RemoveAt(keyNestedElements.Count - 1);

                            string metadataKeyValue = keyElements.Last();
                            List<string> metadataKeyFilter = new(keyNestedElements)
                            {
                                metadataKey
                            };
                            List<string> metadataValueFilter = new(keyNestedElements)
                            {
                                "Value"
                            };

                            var filterKey = Builders<T>.Filter.Eq(string.Join('.', metadataKeyFilter), metadataKeyValue);
                            var filterValue = Builders<T>.Filter.Eq(string.Join('.', metadataValueFilter), queryParams[key]);

                            allFilters.Add(Builders<T>.Filter.And(filterKey, filterValue));
                        }
                    }
                }
            }

            // Then, we need to parse more classical queries
            foreach (string key in queryParams.Keys)
            {
                if (key.Contains("metadata")) continue;

                string value = queryParams[key];
                var filter = Builders<T>.Filter.Eq(key, value);

                allFilters.Add(filter);
            }

            var finalFilter = allFilters.Count > 0 ? Builders<T>.Filter.And(allFilters) : new BsonDocument();

            return await (await collection.FindAsync(finalFilter)).ToListAsync();
        }
    }
}