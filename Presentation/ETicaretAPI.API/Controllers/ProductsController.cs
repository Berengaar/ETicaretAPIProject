using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var products = _productReadRepository.GetAll().ToList();
            if(products.Count>-1)
                return Ok(products);
            else return NoContent();
        }

        [HttpPost]
        [Route("Posting")]
        public async Task<IActionResult> AddAsync(Product product)
        {
            var state=await _productWriteRepository.AddAsync(product);
            if (state)
            {
                return Created("~api/Products", product);
            }
            else return BadRequest("Model is not true");
        }
    }
}
