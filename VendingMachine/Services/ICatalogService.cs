using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Models.Domain;

namespace VendingMachine.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<Catalog>> GetAllCatalogItemsAsync();
        Task<Catalog> GetCatalogItemByIdAsync(int id);
        Task DecreaseQuantityAsync(int id, int quantity);
    }
}
