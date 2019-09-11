using System.Threading.Tasks;
using ApplicationLogic;
using FluentAssertions;
using SimpleInjector;
using Xunit;

namespace Tests.Regular
{
    public class RegularTestWithInitAndCleanup : IAsyncLifetime
    {
        public RegularTestWithInitAndCleanup()
        {
            var container = new Container().RegisterApplicationServices();
            repository = container.GetInstance<IRepository<Invoice>>();
        }

        private readonly IRepository<Invoice> repository;
        private Invoice invoice;

        public async Task InitializeAsync()
        {
            invoice = new Invoice {Paid = false};
            await repository.Create(invoice);
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
            invoice.Paid = true;

            // Act
            await repository.Update(invoice);

            // Assert
            var actualInvoice = await repository.Find(invoice.Id);
            actualInvoice.Paid.Should().BeTrue();
        }
    }
}