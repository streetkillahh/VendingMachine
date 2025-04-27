using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine;
using VendingMachine.Models.Domain;

namespace VendingMachine.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly ApplicationDbContext _context;

        public CatalogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Catalog>> GetAllAsync()
        {
            return await _context.Catalogs.Include(c => c.Brand).ToListAsync();
        }

        public async Task<Catalog> GetByIdAsync(int id)
        {
            return await _context.Catalogs.FindAsync(id);
        }

        public async Task UpdateAsync(Catalog catalog)
        {
            _context.Catalogs.Update(catalog);
            await _context.SaveChangesAsync();
        }
    }
}
