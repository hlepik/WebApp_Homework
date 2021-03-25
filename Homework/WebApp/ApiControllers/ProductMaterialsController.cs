using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductMaterialsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public ProductMaterialsController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/ProductMaterials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductMaterial>>> GetProductMaterials()
        {
            return Ok(await _uow.ProductMaterial.GetAllAsync());
        }

        // GET: api/ProductMaterials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductMaterial>> GetProductMaterial(Guid id)
        {
            var productMaterial = await _context.ProductMaterials.FindAsync(id);

            if (productMaterial == null)
            {
                return NotFound();
            }

            return productMaterial;
        }

        // PUT: api/ProductMaterials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductMaterial(Guid id, ProductMaterial productMaterial)
        {
            if (id != productMaterial.Id)
            {
                return BadRequest();
            }

            _context.Entry(productMaterial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductMaterialExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductMaterials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductMaterial>> PostProductMaterial(ProductMaterial productMaterial)
        {
            _context.ProductMaterials.Add(productMaterial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductMaterial", new { id = productMaterial.Id }, productMaterial);
        }

        // DELETE: api/ProductMaterials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductMaterial(Guid id)
        {
            var productMaterial = await _context.ProductMaterials.FindAsync(id);
            if (productMaterial == null)
            {
                return NotFound();
            }

            _context.ProductMaterials.Remove(productMaterial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductMaterialExists(Guid id)
        {
            return _context.ProductMaterials.Any(e => e.Id == id);
        }
    }
}
