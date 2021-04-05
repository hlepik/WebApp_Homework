using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class AppRoleController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<AppRole> _roleManager;

        public AppRoleController(AppDbContext context, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        // GET: api/AppRole
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppRole>>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        // GET: api/AppRole/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppRole>> GetAppRole(Guid id)
        {
            var appRole = await _roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);

            if (appRole == null)
            {
                return NotFound();
            }

            return appRole;
        }

        // PUT: api/AppRole/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppRole(Guid id, AppRole appRole)
        {
            if (id != appRole.Id)
            {
                return BadRequest();
            }

            appRole.Id = Guid.NewGuid();

            _context.Roles.Update(appRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/AppRole
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppRole>> PostAppRole(AppRole appRole)
        {
            _context.Roles.Add(appRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppRole", new { id = appRole.Id }, appRole);
        }

        // DELETE: api/AppRole/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppRole(Guid id)
        {
            var appRole = await _roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appRole == null)
            {
                return NotFound();
            }

            await _roleManager.DeleteAsync(appRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
