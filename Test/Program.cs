using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Services;

//test is collection exist
var IsExist = await RocketMongo.ConnectMongoAndGetDatabase("mongodb://127.0.0.1:27017", "Test")
    .IsCollectionExistAsync("User");
Console.WriteLine(IsExist);

//get database by name
var Database = RocketMongo.ConnectMongo("mongodb://127.0.0.1:27017")
    .GetDatabase("ModernAfzarPars");

//get collection
var Userdb = RocketMongo.ConnectMongoAndGetDatabase("mongodb://127.0.0.1:27017", "Test")
    .GetCollection<User>("User");

//insert Test
var InsertOneTest = await Userdb.InsertOneToCollectionAsync<User>(new User
{
    FullName = "reza",
    Id = 2,
    Mobile = "09365987474"
});
Console.WriteLine($"Insert Test Pass:{InsertOneTest}");

//Create collectionTest
var AddCollectionTest = await RocketMongo.ConnectMongoAndGetDatabase("mongodb://127.0.0.1:27017", "Test")
    .CreateOneCollectionAsync("Templates");

//create Database Test
var Createdatabase = RocketMongo.ConnectMongo("mongodb://127.0.0.1:27017")
    .CreateOneDatabase("Test1");
if (Createdatabase)
    Console.WriteLine($"Create database:{Createdatabase}");

//insert many Test
var InsertManyTest = await Userdb.InsertManyToCollectionAsync<User>(new List<User>
{
    new User{ FullName = "rezahasan",Id = 2,Mobile = "09365987474"},
    new User{ FullName = "ahmad",Id = 3,Mobile = "09365987474"},
    new User{ FullName = "hadi",Id = 4,Mobile = "09365987474"},
    new User{ FullName = "alireza",Id = 5,Mobile = "09365987474"}
});
Console.WriteLine($"Insert Test Pass:{InsertManyTest}");


//CreateManyCollectionsTest
var CreateManyCollection = await RocketMongo.ConnectMongoAndGetDatabase("mongodb://127.0.0.1:27017", "Test")
    .CreateManyCollectionsAsync(new List<string> { "Users", "Roles", "Products", "Templates", "Customers" });
if (CreateManyCollection)
    Console.WriteLine(CreateManyCollection);




/// <summary>
/// user model 
/// </summary>
public class User
{
    [BsonId]
    public ObjectId _id { get; set; }
    [BsonElement("Id")]
    public int Id { get; set; }
    [BsonElement("Mobile")]
    public string Mobile { get; set; }
    [BsonElement("FullName")]
    public string FullName { get; set; }
}