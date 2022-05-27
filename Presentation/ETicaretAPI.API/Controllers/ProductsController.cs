using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            try
            {
                var products = _productReadRepository.GetAll().ToList();
                if (products.Count > -1)
                    return Ok(products);
                else return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
          
        }

        [HttpPost]
        [Route("SeedPosting")]
        public async Task<IActionResult> AddAsync()
        {
            try
            {
                await _productWriteRepository.AddRangeAsync(new()
                {
                    new() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, CreatedDate = DateTime.UtcNow, Stock = 10 },
                    new() { Id = Guid.NewGuid(), Name = "Product 2", Price = 200, CreatedDate = DateTime.UtcNow, Stock = 5 },
                    new() { Id = Guid.NewGuid(), Name = "Product 3", Price = 300, CreatedDate = DateTime.UtcNow, Stock = 8 },
                });
                await _productWriteRepository.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
    }
}
