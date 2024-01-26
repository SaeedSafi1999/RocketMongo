using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Services;


var Userdb =MongoServices.ConnectMongo("mongodb://127.0.0.1:27017")
    .GetDatabaseByName("Test")
    .GetCollectionByName<User>("User")
    .MakeCollectionAsQueryAble();



var Users = await Userdb.ToListAsync();
foreach (var item in Users)
{
 Console.WriteLine(item.FullName);
}


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