using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Services
{
    public static class MongoServices
    {

        /// <summary>
        /// Use this method for connect and get database
        /// </summary>
        /// <param name="Config"></param>
        /// <param name="DbName"></param>
        /// <returns>Database in mongo server</returns>
        public static IMongoDatabase ConnectMongoAndGetDatabase(string Config, string DbName)
        {
            try
            {
                var client = new MongoClient(Config);
                return client.GetDatabase(DbName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// check collection is exist in database or not
        /// </summary>
        /// <param name="Database"></param>
        /// <param name="CollectionName"></param>
        /// <returns></returns>
        public async static Task<bool> IsCollectionExist(this IMongoDatabase Database,string CollectionName)
        {
            try
            {
                return (await Database.ListCollectionNamesAsync()).ToList().Any(x =>x.Equals(CollectionName));
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Create many collection just by sending the names as list of string
        /// </summary>
        /// <param name="Database"></param>
        /// <param name="CollectionNames"></param>
        /// <returns></returns>
        public static async Task<bool> CreateManyCollectionsAsync(this IMongoDatabase Database,List<string> CollectionNames)
        {
            try
            {
                foreach (var Name in CollectionNames)
                {
                    await Database.CreateCollectionAsync(Name);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// insert one collection to selected database
        /// </summary>
        /// <param name="Database"></param>
        /// <param name="CollectionName"></param>
        /// <returns></returns>
        public async static Task<bool> CreateOneCollection(this IMongoDatabase Database, string CollectionName)
        {
            try
            {
                await Database.CreateCollectionAsync(CollectionName);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        /// <summary>
        /// connect To Mongo
        /// </summary>
        /// <param name="Config"></param>
        /// <returns></returns>
        public static IMongoClient ConnectMongo(string Config)
        {
            try
            {
                return new MongoClient(Config);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// use this method to cerate one database
        /// </summary>
        /// <param name="client"></param>
        /// <param name="name"></param>
        /// <returns>bool</returns>
        public static bool CreateOneDatabase(this IMongoClient client, string name)
        {
            try
            {
                client.GetDatabase(name);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// create collection async
        /// </summary>
        /// <param name="Database"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<bool> CreateCollectionAsync(this IMongoDatabase Database, string name)
        {
            await Database.CreateCollectionAsync(name);
            return true;
        }
        /// <summary>
        /// to get database 
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="DatabaseName"></param>
        /// <returns></returns>
        public static IMongoDatabase GetDatabaseByName(this MongoClient Client, string DatabaseName)
        {
            return Client.GetDatabase(DatabaseName);
        }
        /// <summary>
        /// Get collection
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CollectionName"></param>
        /// <returns></returns>
        public static IMongoCollection<TEntity> GetCollectionByName<TEntity>(this IMongoDatabase db, string CollectionName)
        {
            return db.GetCollection<TEntity>(CollectionName);
        }
        /// <summary>
        /// make collection asqueryable
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IMongoQueryable<TEntity> MakeCollectionAsQueryAble<TEntity>(this IMongoCollection<TEntity> collection)
        {
            return collection.AsQueryable();
        }
        /// <summary>
        /// make queryable to can return all data of collection without expression and filter it with where if you pass value to it
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static async Task<List<TEntity>> Get<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>>? expression = null)
        {
            var query = collection.AsQueryable();
            if (expression != null)
            {
                query.Where(expression);
            }
            return await query.ToListAsync();

        }
        /// <summary>
        /// this method insert to collection
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> InsertOneToCollectionAsync<TEntity>(this IMongoCollection<TEntity> Collection, TEntity Model)
        {
            try
            {
                await Collection.InsertOneAsync(Model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// add many to collection
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="Collection"></param>
        /// <param name="ModelList"></param>
        /// <returns></returns>
        public static async Task<bool> InsertManyToCollectionAsync<TEntity>(this IMongoCollection<TEntity> Collection, List<TEntity> ModelList)
        {
            try
            {
                await Collection.InsertManyAsync(ModelList);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
