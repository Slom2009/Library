using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoExample.Models;

namespace MongoExample.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Book> _bookCollection;
        private readonly IMongoCollection<Author> _authorCollection;


        public MongoDbService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _bookCollection = database.GetCollection<Book>(mongoDbSettings.Value.BooksCollectionName);
            _authorCollection = database.GetCollection<Author>(mongoDbSettings.Value.AuthorsCollectionName);
        }

        #region Author CRUD
        public async Task CreateAuthorAsync(Author author)
        {
            await _authorCollection.InsertOneAsync(author);
            return;
        }
        public async Task UpdateAuthorAsync(string id, Author authorIn)
        {
            await _authorCollection.ReplaceOneAsync(author => author.Id == id, authorIn);
            return;
        }

        public async Task<List<Author>> GetAuthorAsync()
        {
            return await _authorCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task<Author> GetauthorByIdAsync(string id)
        {
            FilterDefinition<Author> filter = Builders<Author>.Filter.Eq("Id", id);
            return _authorCollection.Find(filter).FirstOrDefault();
        }


        public async Task DeleteAuthorAsync(string id)
        {
            FilterDefinition<Author> filter = Builders<Author>.Filter.Eq("Id", id);
            await _authorCollection.DeleteOneAsync(filter);
            return;
        }

        #endregion

        #region Book CRUD
        public async Task AddToBookAsync(string id, string authorId)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq("Id", id);
            UpdateDefinition<Book> update = Builders<Book>.Update.AddToSet<string>("items", authorId);
            await _bookCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task CreateBookAsync(Book book)
        {
            await _bookCollection.InsertOneAsync(book);
            return;
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            return await _bookCollection.Find(new BsonDocument()).ToListAsync();
        }


        public async Task DeleteBookAsync(string id)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq("Id", id);
            await _bookCollection.DeleteOneAsync(filter);
            return;
        }

        public async Task UpdateBookAsync(string id, Book bookIn)
        {
            await _bookCollection.ReplaceOneAsync(book => book.Id == id, bookIn);
            return;
        }

        public async Task<Book> GetBookByIdAsync(string id)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq("Id", id);
            return _bookCollection.Find(filter).FirstOrDefault();
        }

        public async Task AddAuthorToBook(string bookId, string authorId)
        {
            FilterDefinition<Book> filter = Builders<Book>.Filter.Eq("Id",bookId);
            UpdateDefinition<Book> update = Builders<Book>.Update.Set(b => b.AuthorId, authorId);
            await _bookCollection.UpdateOneAsync(filter,update);
            return;
        }
        #endregion
    }
}
