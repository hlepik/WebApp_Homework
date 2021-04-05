using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class MaterialsController : ControllerBase
    {

        private readonly IAppBLL _bll;

        public MaterialsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Materials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BLL.App.DTO.Material>>> GetMaterials()
        {
            return Ok(await _bll.Material.GetAllAsync());
        }

        // GET: api/Materials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BLL.App.DTO.Material>> GetMaterial(Guid id)
        {
            var material = await _bll.Material.FirstOrDefaultAsync(id);

            if (material == null)
            {
                return NotFound();
            }

            return material;
        }

        // PUT: api/Materials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterial(Guid id, BLL.App.DTO.Material material)
        {
            if (id != material.Id)
            {
                return BadRequest();
            }

            _bll.Material.Update(material);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Materials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Material>> PostMaterial(BLL.App.DTO.Material material)
        {
            _bll.Material.Add(material);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetMaterial", new { id = material.Id }, material);
        }

        // DELETE: api/Materials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(Guid id)
        {
            var material = await _bll.Material.FirstOrDefaultAsync(id);
            if (material == null)
            {
                return NotFound();
            }

            _bll.Material.Remove(material);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
