using System.Threading.Tasks;
using MongoDB.Bson;

namespace ApplicationLogic
{
    public interface IRepository<T>
    {
        Task<T> Find(ObjectId id);
        Task Create(T entity);
        Task Update(T entity);
    }
}