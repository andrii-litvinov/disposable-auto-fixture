using System.Threading.Tasks;
using ApplicationLogic;
using FluentAssertions;
using Tests.Async.Conventions;
using Xunit;

namespace Tests.Async
{
    public class AsyncAutoTest : Primitives.Async
    {
        [Theory, ExistingUnpaidInvoiceConventions]
        public async Task ShouldUpdateInvoice(Invoice invoice, IRepository<Invoice> repository)
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