using System.Threading.Tasks;
using ApplicationLogic;
using FluentAssertions;
using SimpleInjector;
using Xunit;

namespace Tests
{
    public class RegularTestsWithCleanup : IAsyncLifetime
    {
        public RegularTestsWithCleanup()
        {
            var container = new Container().RegisterApplicationServices();
            repository = container.GetInstance<IRepository<Invoice>>();
        }

        private readonly IRepository<Invoice> repository;
        private Invoice invoice;

        public async Task InitializeAsync()
        {
        }

        public async Task DisposeAsync()
        {
            if (invoice != null)
                await repository.Delete(invoice.Id);
        }

        [Fact]
        public async Task ShouldUpdateInvoice()
        {
            // Arrange
            invoice = new Invoice {Paid = false};
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