using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MongoServices
    {
        /// <summary>
        /// Use this method for connect and get database
        /// </summary>
        /// <param name="Config"></param>
        /// <param name="DbName"></param>
        /// <returns>Database in mongo server</returns>
        public sealed override MongoClient ConnectMongo(string Config)
        {
            try
            {
                return new MongoClient(Config);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to MongoDB: " + ex.Message);
                return null;
            }
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
        /// Get And make it queryAble
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
        public static async Task<List<TEntity>> Get<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>>? expression =null)
        {
            var query = collection.AsQueryable();
            if (expression!=null)
            {
                query.Where(expression);
            }
            return await query.ToListAsync();
                
        }

    }
}
