using MongoDB.Bson;

namespace ApplicationLogic
{
    public class Invoice
    {
        public ObjectId Id { get; set; }
        public decimal Amount { get; set; }
        public bool Paid { get; set; }
    }
}