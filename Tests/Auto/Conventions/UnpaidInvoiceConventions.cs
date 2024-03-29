using ApplicationLogic;
using AutoFixture;
using AutoFixture.Xunit2;
using MongoDB.Bson;
using Primitives;
using SimpleInjector;

namespace Tests.Auto.Conventions
{
    public class UnpaidInvoiceConventions : AutoDataAttribute
    {
        public UnpaidInvoiceConventions() : base(ConfigureFixture)
        {
        }

        private static IFixture ConfigureFixture()
        {
            var container = new Container().RegisterApplicationServices();
            var fixture = new Fixture().Container(container);

            fixture.Register(ObjectId.GenerateNewId);
            fixture.Customize<Invoice>(composer => composer
                .Without(invoice => invoice.Paid)
            );
            
            return fixture;
        }
    }
}