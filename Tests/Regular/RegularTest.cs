using System.Threading.Tasks;
using ApplicationLogic;
using FluentAssertions;
using SimpleInjector;
using Xunit;

namespace Tests.Regular
{
    public class RegularTest
    {
        public RegularTest()
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
            invoice.Paid = true;

            // Act
            await repository.Update(invoice);

            // Assert
            var actualInvoice = await repository.Find(invoice.Id);
            actualInvoice.Paid.Should().BeTrue();
        }
    }
}