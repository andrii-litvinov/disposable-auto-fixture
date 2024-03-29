using System.Threading.Tasks;
using ApplicationLogic;
using FluentAssertions;
using Tests.Auto.Conventions;
using Xunit;

namespace Tests.Auto
{
    public class RegularAutoTest
    {
        [Theory, UnpaidInvoiceConventions]
        public async Task ShouldUpdateInvoice(Invoice invoice, IRepository<Invoice> repository)
        {
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