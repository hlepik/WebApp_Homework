using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Category = Domain.App.Category;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class CategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public CategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BLL.App.DTO.Category>>> GetCategories()
        {
            return Ok(await _bll.Category.GetAllCategoriesAsync());
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BLL.App.DTO.Category>> GetCategory(Guid id)
        {
            var category = await _bll.Category.FirstOrDefaultAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, BLL.App.DTO.Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _bll.Category.Update(category);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(BLL.App.DTO.Category category)
        {
            _bll.Category.Add(category);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _bll.Category.FirstOrDefaultAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _bll.Category.Remove(category);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
