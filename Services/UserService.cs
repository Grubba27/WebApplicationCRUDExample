using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApplicationCRUDExample.Models;
using WebApplicationCRUDExample.Services.DB;

namespace WebApplicationCRUDExample.Services;

public class UserService
{
    private readonly IMongoCollection<User> _usersCollection;

    public UserService(
        IOptions<MongoDBSettings> library)
    {
        var mongoClient = new MongoClient(
            library.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            library.Value.DataBaseName);

        _usersCollection = mongoDatabase.GetCollection<User>(
            library.Value.UsersCollectionName);
    }

    public async Task<List<User>> GetUserAsync() =>
        await _usersCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetUserByIdAsync(string id) =>
        await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateUserAsync(User newUser) =>
        await _usersCollection.InsertOneAsync(newUser);

    public async Task UpdateUserAsync(string id, User updatedUser) =>
        await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task RemoveUserAsync(string id) =>
        await _usersCollection.DeleteOneAsync(x => x.Id == id);
}