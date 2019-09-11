using MongoDB.Driver;
using SimpleInjector;

namespace ApplicationLogic
{
    public static class Bootstrapper
    {
        public static Container RegisterApplicationServices(this Container container)
        {
            var client = new MongoClient();

            container.RegisterInstance(client.GetDatabase("demo"));
            container.Register(typeof(IRepository<>), typeof(Bootstrapper).Assembly);
            
            return container;
        }
    }
}