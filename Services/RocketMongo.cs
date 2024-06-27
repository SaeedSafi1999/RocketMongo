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
    public static class RocketMongo
    {
        /// <summary>
        /// Connects to a MongoDB server and retrieves the specified database.
        /// </summary>
        /// <param name="Config">The connection string for the MongoDB server.</param>
        /// <param name="DbName">The name of the database to retrieve.</param>
        /// <returns>Returns the specified database from the MongoDB server, or null if an error occurs.</returns>
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
        /// Checks if a collection exists in the specified database.
        /// </summary>
        /// <param name="Database">The database to check for the collection.</param>
        /// <param name="CollectionName">The name of the collection to check for.</param>
        /// <returns>Returns true if the collection exists, otherwise false.</returns>
        public async static Task<bool> IsCollectionExistAsync(this IMongoDatabase Database, string CollectionName)
        {
            try
            {
                return (await Database.ListCollectionNamesAsync()).ToList().Any(x => x.Equals(CollectionName));
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Creates multiple collections in the specified database.
        /// </summary>
        /// <param name="Database">The database in which to create the collections.</param>
        /// <param name="CollectionNames">A list of names of the collections to create.</param>
        /// <returns>Returns true if all collections are created successfully, otherwise false.</returns>
        public static async Task<bool> CreateManyCollectionsAsync(this IMongoDatabase Database, List<string> CollectionNames)
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
        /// Creates a single collection in the specified database.
        /// </summary>
        /// <param name="Database">The database in which to create the collection.</param>
        /// <param name="CollectionName">The name of the collection to create.</param>
        /// <returns>Returns true if the collection is created successfully, otherwise false.</returns>
        public async static Task<bool> CreateOneCollectionAsync(this IMongoDatabase Database, string CollectionName)
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
        /// Connects to a MongoDB server.
        /// </summary>
        /// <param name="Config">The connection string for the MongoDB server.</param>
        /// <returns>Returns a MongoDB client object, or null if an error occurs.</returns>
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
        /// Creates a new database on the MongoDB server.
        /// </summary>
        /// <param name="client">The MongoDB client.</param>
        /// <param name="name">The name of the database to create.</param>
        /// <returns>Returns true if the database is created successfully, otherwise false.</returns>
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
        /// Creates a collection in the specified database.
        /// </summary>
        /// <param name="Database">The database in which to create the collection.</param>
        /// <param name="name">The name of the collection to create.</param>
        /// <returns>Returns true if the collection is created successfully.</returns>
        public static async Task<bool> CreateCollectionAsync(this IMongoDatabase Database, string name)
        {
            await Database.CreateCollectionAsync(name);
            return true;
        }

        /// <summary>
        /// Retrieves a database by its name from the MongoDB client.
        /// </summary>
        /// <param name="Client">The MongoDB client.</param>
        /// <param name="DatabaseName">The name of the database to retrieve.</param>
        /// <returns>Returns the specified database.</returns>
        public static IMongoDatabase GetDatabaseByName(this MongoClient Client, string DatabaseName)
        {
            return Client.GetDatabase(DatabaseName);
        }

        /// <summary>
        /// Retrieves a collection from the specified database.
        /// </summary>
        /// <typeparam name="TEntity">The type of the collection entity.</typeparam>
        /// <param name="db">The database from which to retrieve the collection.</param>
        /// <param name="CollectionName">The name of the collection to retrieve.</param>
        /// <returns>Returns the specified collection.</returns>
        public static IMongoCollection<TEntity> GetCollectionByName<TEntity>(this IMongoDatabase db, string CollectionName)
        {
            return db.GetCollection<TEntity>(CollectionName);
        }

        /// <summary>
        /// Makes a collection queryable.
        /// </summary>
        /// <typeparam name="TEntity">The type of the collection entity.</typeparam>
        /// <param name="collection">The collection to make queryable.</param>
        /// <returns>Returns a queryable version of the collection.</returns>
        public static IMongoQueryable<TEntity> MakeCollectionAsQueryAble<TEntity>(this IMongoCollection<TEntity> collection)
        {
            return collection.AsQueryable();
        }

        /// <summary>
        /// Retrieves all data from a collection, optionally filtering with an expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the collection entity.</typeparam>
        /// <param name="collection">The collection to query.</param>
        /// <param name="expression">An optional filter expression.</param>
        /// <returns>Returns a list of entities from the collection.</returns>
        public static async Task<List<TEntity>> GetAsync<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>>? expression = null)
        {
            var query = collection.AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return await query.ToListAsync();
        }

        /// <summary>
        /// Inserts a single document into the specified collection.
        /// </summary>
        /// <typeparam name="TEntity">The type of the document entity.</typeparam>
        /// <param name="Collection">The collection into which to insert the document.</param>
        /// <param name="Model">The document to insert.</param>
        /// <returns>Returns true if the document is inserted successfully, otherwise false.</returns>
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
        /// Inserts multiple documents into the specified collection.
        /// </summary>
        /// <typeparam name="TEntity">The type of the document entity.</typeparam>
        /// <param name="Collection">The collection into which to insert the documents.</param>
        /// <param name="ModelList">The list of documents to insert.</param>
        /// <returns>Returns true if the documents are inserted successfully, otherwise false.</returns>
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

        /// <summary>
        /// Deletes a single document from the specified collection based on a filter expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the document entity.</typeparam>
        /// <param name="Collection">The collection from which to delete the document.</param>
        /// <param name="expression">The filter expression to identify the document to delete.</param>
        /// <returns>Returns true if the document is deleted successfully, otherwise false.</returns>
        public static async Task<bool> DeleteOneFromCollectionAsync<TEntity>(this IMongoCollection<TEntity> Collection, Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var query = Builders<TEntity>.Filter.Where(expression);
                await Collection.DeleteOneAsync(query);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes multiple documents from the specified collection based on a filter expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the document entity.</typeparam>
        /// <param name="Collection">The collection from which to delete the documents.</param>
        /// <param name="expression">The filter expression to identify the documents to delete.</param>
        /// <returns>Returns true if the documents are deleted successfully, otherwise false.</returns>
        public static async Task<bool> DeleteManyFromCollectionAsync<TEntity>(this IMongoCollection<TEntity> Collection, Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var query = Builders<TEntity>.Filter.Where(expression);
                await Collection.DeleteManyAsync(query);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
