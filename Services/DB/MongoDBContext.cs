using MongoDB.Driver;
using WebApplicationCRUDExample.Models;

namespace WebApplicationCRUDExample.Services.DB
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string DataBaseName { get; set; }
        public string BooksCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
    }
}