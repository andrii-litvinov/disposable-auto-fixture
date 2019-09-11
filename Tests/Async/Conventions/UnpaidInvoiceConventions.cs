using ApplicationLogic;
using AutoFixture;
using AutoFixture.Xunit2;
using MongoDB.Bson;
using Primitives;
using SimpleInjector;
using Tests.Async.Initializers;

namespace Tests.Async.Conventions
{
    public class ExistingUnpaidInvoiceConventions : AutoDataAttribute
    {
        public ExistingUnpaidInvoiceConventions() : base(ConfigureFixture)
        {
        }

        private static IFixture ConfigureFixture()
        {
            var container = new Container().RegisterApplicationServices();
            var fixture = new Fixture().Container(container).Async();

            fixture.Register(ObjectId.GenerateNewId);
            fixture.Customize<Invoice>(composer => composer
                .Without(invoice => invoice.Paid)
            );
            fixture.Freeze<Invoice>();
            fixture.RegisterAsync<ExistingInvoiceInitializer>();

            return fixture;
        }
    }
}