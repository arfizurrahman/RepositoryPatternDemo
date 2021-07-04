using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryPatternDemo.Entities;
using RepositoryPatternDemo.Interfaces;
using RepositoryPatternDemo.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryPatternDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();

            return Ok(products);
        }

        [HttpGet("ProductsByCategory")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            var products = await _productRepository.GetByConditionAsync(x => x.Category.Equals(category));

            return Ok(products);
        }

        [HttpGet("{id}", Name = "ProductById")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepository.GetAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            _productRepository.Add(product);

            await _productRepository.SaveAsync();

            return CreatedAtRoute("ProductById", new { id = product.Id },
                product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetAsync(id);

            _productRepository.Delete(product);

            await _productRepository.SaveAsync();
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product
            product)
        {
            _productRepository.Update(product);

            await _productRepository.SaveAsync();

            return NoContent();
        }
    }
}
