using System.Threading.Tasks;
using ApplicationLogic;
using Primitives;

namespace Tests.Async.Initializers
{
    public class ExistingInvoiceInitializer : Disposable
    {
        private readonly Invoice invoice;
        private readonly IRepository<Invoice> repository;

        public ExistingInvoiceInitializer(Invoice invoice, IRepository<Invoice> repository)
        {
            this.invoice = invoice;
            this.repository = repository;
        }

        protected override async Task Initialize()
        {
            await repository.Create(invoice);
            OnDispose += () => repository.Delete(invoice.Id);
        }
    }
}