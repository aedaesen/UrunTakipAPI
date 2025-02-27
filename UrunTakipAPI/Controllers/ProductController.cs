using Microsoft.AspNetCore.Mvc;
using UrunTakip.Core.Entities;
using UrunTakip.Core.Repositories;

namespace UrunTakipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;

        public ProductController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _productRepository.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _productRepository.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            await _productRepository.Add(product);
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            await _productRepository.Update(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productRepository.Delete(id);
            return Ok();
        }

        [HttpGet("filter")]
      public async Task<IActionResult> FilterProducts(
               [FromQuery] int? categoryId, 
               [FromQuery] decimal? minPrice, 
               [FromQuery] decimal? maxPrice,
               [FromQuery] int? minStock,
               [FromQuery] int? maxStock)


         {
             var query = _productRepository.GetAllQueryable(); 

                    if (categoryId.HasValue)
                       query = query.Where(p => p.CategoryId == categoryId.Value);

                 if (minPrice.HasValue)
                       query = query.Where(p => p.Price >= minPrice.Value);

                 if (maxPrice.HasValue)
                       query = query.Where(p => p.Price <= maxPrice.Value);

                 if (minStock.HasValue)
                       query = query.Where(p => p.Stock >= minStock.Value);

                 if (maxStock.HasValue)
                       query = query.Where(p => p.Stock <= maxStock.Value);

                 var result = await query.ToListAsync();
                 return Ok(result);
}


    }
}
