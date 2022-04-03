using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApplicationCRUDExample.Models;
using WebApplicationCRUDExample.Services.DB;

namespace WebApplicationCRUDExample.Services;

public class LibraryService
{
    private readonly IMongoCollection<Book> _booksCollection;

    public LibraryService(
        IOptions<MongoDBSettings> library)
    {
        var mongoClient = new MongoClient(
            library.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            library.Value.DataBaseName);

        _booksCollection = mongoDatabase.GetCollection<Book>(
            library.Value.BooksCollectionName);
    }

    public async Task<List<Book>> GetBookAsync()
    {
        return await _booksCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(string id)
    {
        return await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateBookAsync(Book newBook)
    {
        await _booksCollection.InsertOneAsync(newBook);
    }

    public async Task UpdateBookAsync(string id, Book updatedBook)
    {
        await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);
    }

    public async Task RemoveBookAsync(string id)
    {
        await _booksCollection.DeleteOneAsync(x => x.Id == id);
    }
}