using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Product = BLL.App.DTO.Product;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ProductsController : ControllerBase
    {
        private readonly IAppBLL _bll;


        public ProductsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BLL.App.DTO.Product>>> GetProducts()
        {
            return Ok(await _bll.Product.GetAllProductsAsync(User.GetUserId()!.Value));
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _bll.Product.FirstOrDefaultDTOAsync(id.Value);

            return Ok(product);

        }


        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            product.AppUserId = User.GetUserId()!.Value;
            product.DateAdded = DateTime.Now.Date;
            _bll.Product.Update(product);
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _bll.Product.Add(product);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }


        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _bll.Product.FirstOrDefaultDTOAsync(id);


            _bll.Product.Remove(product);
            await _bll.SaveChangesAsync();

            return Ok(product);

        }
    }
}
