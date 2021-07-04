using Microsoft.EntityFrameworkCore;
using RepositoryPatternDemo.Entities;
using RepositoryPatternDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryPatternDemo.Persistence.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Product>> GetTopFiveHighestRatedProduct()
        {
            return await _context.Products.Where(x => x.Rating == 5).Take(5).ToListAsync();
        }
    }
}
