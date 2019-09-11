using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ApplicationLogic
{
    public class InvoiceRepository : IRepository<Invoice>
    {
        private readonly IMongoCollection<Invoice> collection;

        public InvoiceRepository(IMongoDatabase database) =>
            collection = database.GetCollection<Invoice>("invoices");

        public async Task<Invoice> Find(ObjectId id) =>
            await collection.Find(invoice => invoice.Id == id).FirstOrDefaultAsync();

        public async Task Create(Invoice entity) => await collection.InsertOneAsync(entity);

        public async Task Update(Invoice entity) =>
            await collection.ReplaceOneAsync(invoice => invoice.Id == entity.Id, entity);
    }
}