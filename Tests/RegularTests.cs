using System.Threading.Tasks;
using ApplicationLogic;
using FluentAssertions;
using SimpleInjector;
using Xunit;

namespace Tests
{
    public class RegularTests
    {
        public RegularTests()
        {
            var container = new Container().RegisterApplicationServices();
            repository = container.GetInstance<IRepository<Invoice>>();
        }

        private readonly IRepository<Invoice> repository;

        [Fact]
        public async Task ShouldUpdateInvoice()
        {
            // Arrange
            var invoice = new Invoice {Paid = false};
            await repository.Create(invoice);

            // Act
            invoice.Paid = true;
            await repository.Update(invoice);

            // Assert
            var actualInvoice = await repository.Find(invoice.Id);
            actualInvoice.Paid.Should().BeTrue();
        }
    }
}