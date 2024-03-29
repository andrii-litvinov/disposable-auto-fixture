using AutoFixture;
using Primitives.SimpleInjector;
using SimpleInjector;
using Xunit;

namespace Primitives
{
    public static class CustomizationExtensions
    {
        public static IFixture Container(this IFixture fixture, Container container)
        {
            container.Options.AllowOverridingRegistrations = true;
            fixture.Inject(container);
            fixture.Customizations.Add(new ContainerSpecimenBuilder(container));
            return fixture;
        }

        public static IFixture InlineData(this IFixture fixture, object[] values)
        {
            fixture.Customizations.Add(new InlineDataSpecimenBuilder(values));
            return fixture;
        }

        public static IFixture Async(this IFixture fixture)
        {
            fixture.Customize(new AsyncCustomization());
            return fixture;
        }
        
        public static IFixture RegisterAsync<T>(this IFixture fixture, T instance) where T : IAsyncLifetime
        {
            AsyncContext.Add(instance);
            fixture.Inject(instance);
            return fixture;
        }

        public static IFixture RegisterAsync<T>(this IFixture fixture) where T : IAsyncLifetime
        {
            var instance = fixture.Create<T>();
            AsyncContext.Add(instance);
            return fixture;
        }
    }
}
