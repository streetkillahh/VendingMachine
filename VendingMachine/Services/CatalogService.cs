using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Models.Domain;
using VendingMachine.Repositories;

namespace VendingMachine.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository _catalogRepository;

        public CatalogService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public async Task<IEnumerable<Catalog>> GetAllCatalogItemsAsync()
        {
            return await _catalogRepository.GetAllAsync();
        }

        public async Task<Catalog> GetCatalogItemByIdAsync(int id)
        {
            return await _catalogRepository.GetByIdAsync(id);
        }

        public async Task DecreaseQuantityAsync(int id, int quantity)
        {
            var item = await _catalogRepository.GetByIdAsync(id);
            if (item != null && item.Quantity >= quantity)
            {
                item.Quantity -= quantity;
                await _catalogRepository.UpdateAsync(item);
            }
        }
    }
}
