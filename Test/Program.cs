using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Services;

//make collection
var Userdb = MongoServices.ConnectMongoAndGetDatabase("mongodb://127.0.0.1:27017", "Test")
    .GetCollection<User>("User");

//insert Test
var InsertOneTest = await Userdb.InsertOneToCollectionAsync<User>(new User
{
    FullName = "reza",
    Id = 2,
    Mobile = "09365987474"
});
Console.WriteLine($"Insert Test Pass:{InsertOneTest}");


//insert many Test
var InsertManyTest = await Userdb.InsertManyToCollectionAsync<User>(new List<User>
{
    new User{ FullName = "rezahasan",Id = 3,Mobile = "09365987474"},
    new User{ FullName = "ahmad",Id = 3,Mobile = "09365987474"},
    new User{ FullName = "hadi",Id = 4,Mobile = "09365987474"},
    new User{ FullName = "alireza",Id = 5,Mobile = "09365987474"}
});
Console.WriteLine($"Insert Test Pass:{InsertManyTest}");




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