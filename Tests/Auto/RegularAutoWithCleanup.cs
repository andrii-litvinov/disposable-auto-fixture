using System.Threading.Tasks;
using ApplicationLogic;
using FluentAssertions;
using Xunit;

namespace Tests.Auto
{
    public class RegularAutoWithCleanup : IAsyncLifetime
    {
        private Invoice invoice;
        private IRepository<Invoice> repository;

        public async Task InitializeAsync()
        {
        }

        public async Task DisposeAsync() => await repository.Delete(invoice.Id);

        [Theory, UnpaidInvoiceConventions]
        public async Task ShouldUpdateInvoice(Invoice invoice, IRepository<Invoice> repository)
        {
            this.invoice = invoice;
            this.repository = repository;

            // Arrange
            await repository.Create(invoice);
            invoice.Paid = true;

            // Act
            await repository.Update(invoice);

            // Assert
            var actualInvoice = await repository.Find(invoice.Id);
            actualInvoice.Paid.Should().BeTrue();
        }
    }
}