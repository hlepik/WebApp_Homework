using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ProductMaterial = Domain.App.ProductMaterial;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]



    public class ProductMaterialsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ProductMaterialsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ProductMaterials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BLL.App.DTO.ProductMaterial>>> GetProductMaterials()
        {
            return Ok(await _bll.ProductMaterial.GetAllProductMaterialsAsync(User.GetUserId()!.Value));
        }

        // GET: api/ProductMaterials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BLL.App.DTO.ProductMaterial>> GetProductMaterial(Guid id)
        {
            var productMaterial = await _bll.ProductMaterial.FirstOrDefaultDTOAsync(id, User.GetUserId()!.Value);

            return Ok(productMaterial);
        }

        // PUT: api/ProductMaterials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductMaterial(Guid id, BLL.App.DTO.ProductMaterial productMaterial)
        {
            if (id != productMaterial.Id)
            {
                return BadRequest();
            }

            _bll.ProductMaterial.Update(productMaterial);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ProductMaterials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductMaterial>> PostProductMaterial(BLL.App.DTO.ProductMaterial productMaterial)
        {
            _bll.ProductMaterial.Add(productMaterial);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProductMaterial", new { id = productMaterial.Id }, productMaterial);
        }

        // DELETE: api/ProductMaterials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductMaterial(Guid id)
        {
            var productMaterial = await _bll.ProductMaterial.FirstOrDefaultDTOAsync(id, User.GetUserId()!.Value);

            _bll.ProductMaterial.Remove(productMaterial);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
