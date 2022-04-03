using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplicationCRUDExample.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; }

    public List<string>? UserLikes { get; set; }
}
