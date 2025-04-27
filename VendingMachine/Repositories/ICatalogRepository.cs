using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Models.Domain;

namespace VendingMachine.Repositories
{
    public interface ICatalogRepository
    {
        Task<IEnumerable<Catalog>> GetAllAsync();
        Task<Catalog> GetByIdAsync(int id);
        Task UpdateAsync(Catalog catalog);
    }
}
