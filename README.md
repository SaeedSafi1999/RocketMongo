# RocketMongo

RocketMongo is a utility library designed to simplify interactions with MongoDB. This library provides methods to connect to MongoDB, manage databases and collections, and perform common database operations asynchronously.

## Installation

Install the RocketMongo package via NuGet Package Manager Console:

    Install-Package SaeedSafi.RocketMongo

## Examples
this is real example usage of this package
connect to mongo

    var DB = await RocketMongo.ConnectMongoAndGetDatabase("mongodb://127.0.0.1:27017","Test")

do what ever you want. now have access on all of method you need
## Usage

### ConnectMongoAndGetDatabase

Connects to a MongoDB server and retrieves the specified database.

#### Signature

csharp

Copy code

`public static IMongoDatabase ConnectMongoAndGetDatabase(string Config, string DbName)` 

#### Parameters

-   `Config` (string): The connection string for the MongoDB server.
-   `DbName` (string): The name of the database to retrieve.

#### Returns

IMongoDatabase: The specified database from the MongoDB server, or null if an error occurs.

### IsCollectionExistAsync

Checks if a collection exists in the specified database asynchronously.

#### Signature

csharp

Copy code

`public async static Task<bool> IsCollectionExistAsync(this IMongoDatabase Database, string CollectionName)` 

#### Parameters

-   `Database` (IMongoDatabase): The database to check for the collection.
-   `CollectionName` (string): The name of the collection to check for.

#### Returns

Task<bool>: A task representing the asynchronous operation that returns true if the collection exists, otherwise false.

### CreateManyCollectionsAsync

Creates multiple collections in the specified database asynchronously.

#### Signature
`public static async Task<bool> CreateManyCollectionsAsync(this IMongoDatabase Database, List<string> CollectionNames)` 

#### Parameters

-   `Database` (IMongoDatabase): The database in which to create the collections.
-   `CollectionNames` (List<string>): A list of names of the collections to create.

#### Returns

Task<bool>: A task representing the asynchronous operation that returns true if all collections are created successfully, otherwise false.

### CreateOneCollectionAsync

Creates a single collection in the specified database asynchronously.

#### Signature

`public async static Task<bool> CreateOneCollectionAsync(this IMongoDatabase Database, string CollectionName)` 

#### Parameters

-   `Database` (IMongoDatabase): The database in which to create the collection.
-   `CollectionName` (string): The name of the collection to create.

#### Returns

Task<bool>: A task representing the asynchronous operation that returns true if the collection is created successfully, otherwise false.

### ConnectMongo

Connects to a MongoDB server.

#### Signature


`public static IMongoClient ConnectMongo(string Config)` 

#### Parameters

-   `Config` (string): The connection string for the MongoDB server.

#### Returns

IMongoClient: The MongoDB client object, or null if an error occurs.

### CreateOneDatabase

Creates a new database on the MongoDB server.

#### Signature

`public static bool CreateOneDatabase(this IMongoClient client, string name)` 

#### Parameters

-   `client` (IMongoClient): The MongoDB client.
-   `name` (string): The name of the database to create.

#### Returns

bool: True if the database is created successfully, otherwise false.

### CreateCollectionAsync

Creates a collection in the specified database asynchronously.

#### Signature


`public static async Task<bool> CreateCollectionAsync(this IMongoDatabase Database, string name)` 

#### Parameters

-   `Database` (IMongoDatabase): The database in which to create the collection.
-   `name` (string): The name of the collection to create.

#### Returns

Task<bool>: A task representing the asynchronous operation that returns true if the collection is created successfully.

### GetDatabaseByName

Retrieves a database by its name from the MongoDB client.

#### Signature

`public static IMongoDatabase GetDatabaseByName(this MongoClient Client, string DatabaseName)` 

#### Parameters

-   `Client` (MongoClient): The MongoDB client.
-   `DatabaseName` (string): The name of the database to retrieve.

#### Returns

IMongoDatabase: The specified database.

### GetCollectionByName

Retrieves a collection from the specified database.

#### Signature

`public static IMongoCollection<TEntity> GetCollectionByName<TEntity>(this IMongoDatabase db, string CollectionName)` 

#### Parameters

-   `db` (IMongoDatabase): The database from which to retrieve the collection.
-   `CollectionName` (string): The name of the collection to retrieve.

#### Returns

IMongoCollection<TEntity>: The specified collection.

### MakeCollectionAsQueryAble

Makes a collection queryable.

#### Signature

`public static IMongoQueryable<TEntity> MakeCollectionAsQueryAble<TEntity>(this IMongoCollection<TEntity> collection)` 

#### Parameters

-   `collection` (IMongoCollection<TEntity>): The collection to make queryable.

#### Returns

IMongoQueryable<TEntity>: A queryable version of the collection.

### GetAsync

Retrieves all data from a collection asynchronously, optionally filtering with an expression.

#### Signature

`public static async Task<List<TEntity>> GetAsync<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>>? expression = null)` 

#### Parameters

-   `collection` (IMongoCollection<TEntity>): The collection to query.
-   `expression` (Expression<Func<TEntity, bool>>?): An optional filter expression.

#### Returns

Task<List<TEntity>>: A task representing the asynchronous operation that returns a list of entities from the collection.

### InsertOneToCollectionAsync

Inserts a single document into the specified collection asynchronously.

#### Signature

`public static async Task<bool> InsertOneToCollectionAsync<TEntity>(this IMongoCollection<TEntity> Collection, TEntity Model)` 

#### Parameters

-   `Collection` (IMongoCollection<TEntity>): The collection into which to insert the document.
-   `Model` (TEntity): The document to insert.

#### Returns

Task<bool>: A task representing the asynchronous operation that returns true if the document is inserted successfully, otherwise false.

### InsertManyToCollectionAsync

Inserts multiple documents into the specified collection asynchronously.

#### Signature
`public static async Task<bool> InsertManyToCollectionAsync<TEntity>(this IMongoCollection<TEntity> Collection, List<TEntity> ModelList)` 

#### Parameters

-   `Collection` (IMongoCollection<TEntity>): The collection into which to insert the documents.
-   `ModelList` (List<TEntity>): The list of documents to insert.

#### Returns

Task<bool>: A task representing the asynchronous operation that returns true if the documents are inserted successfully, otherwise false.

### DeleteOneFromCollectionAsync

Deletes a single document from the specified collection asynchronously based on a filter expression.

#### Signature

`public static async Task<bool> DeleteOneFromCollectionAsync<TEntity>(this IMongoCollection<TEntity> Collection, Expression<Func<TEntity, bool>> expression)` 

#### Parameters

-   `Collection` (IMongoCollection<TEntity>): The collection from which to delete the document.
-   `expression` (Expression<Func<TEntity, bool>>): The filter expression to identify the document to delete.

#### Returns

Task<bool>: A task representing the asynchronous operation that returns true if the document is deleted successfully, otherwise false.

### DeleteManyFromCollectionAsync

Deletes multiple documents from the specified collection asynchronously based on a filter expression.

#### Signature

`public static async Task<bool> DeleteManyFromCollectionAsync<TEntity>(this IMongoCollection<TEntity> Collection, Expression<Func<TEntity, bool>> expression)` 

#### Parameters

-   `Collection` (IMongoCollection<TEntity>): The collection from which to delete the documents.
-   `expression` (Expression<Func<TEntity, bool>>): The filter expression to identify the documents to delete.
