using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryPatternDemo.Entities;
using RepositoryPatternDemo.Interfaces.Repositories;
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
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            return Ok(products);
        }

        [HttpGet("ProductsByCategory")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            var products = await _unitOfWork.Products.GetByConditionAsync(x => x.Category.Equals(category));

            return Ok(products);
        }

        [HttpGet("{id}", Name = "ProductById")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _unitOfWork.Products.GetAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            _unitOfWork.Products.Add(product);

            await _unitOfWork.SaveAsync();

            return CreatedAtRoute("ProductById", new { id = product.Id },
                product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Products.GetAsync(id);

            _unitOfWork.Products.Delete(product);

            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product
            product)
        {
            _unitOfWork.Products.Update(product);

            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
