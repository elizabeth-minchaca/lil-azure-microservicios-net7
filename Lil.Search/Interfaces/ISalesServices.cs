using Lil.Search.Models;

namespace Lil.Search.Interfaces
{
    public interface ISalesServices
    {
        Task<ICollection<Order>> GetAsync(string customerId);

    }
}
