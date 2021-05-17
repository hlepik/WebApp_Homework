using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PublicApi.DTO.v1;

namespace WebApp.Areas.Admin.ApiControllers
{
    /// <summary>
    /// Api controller for AppRole
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppRoleController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public AppRoleController(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        /// <summary>
        /// Returns all roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppRole>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        // GET: api/AppRole/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppRole>> GetAppRole(Guid id)
        {
            var appRole = await _context.Roles.FindAsync(id);

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

            _context.Entry(appRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppRoleExists(id))
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
            var appRole = await _context.Roles.FindAsync(id);
            if (appRole == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(appRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Return all users in role.
        /// </summary>
        /// <returns></returns>
        [HttpGet ("Members/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Product), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AppUser>>>  GetUserWithRole(Guid id)
        {
            var appUser = await _context.Users.ToListAsync();
            var role = await _roleManager.FindByIdAsync(id.ToString());
            var members = new List<AppUser>();
            if (appUser == null)
            {
                return NotFound();
            }

            foreach (var user in appUser)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    members.Add(user);
                }
            }
            return members;
        }
        /// <summary>
        /// Return all users not in role.
        /// </summary>
        /// <returns></returns>
        [HttpGet ("NonMembers/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Product), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AppUser>>>  GetUserWithNoRole(Guid id)
        {
            var appUser = await _context.Users.ToListAsync();
            var role = await _roleManager.FindByIdAsync(id.ToString());
            var nonMembers = new List<AppUser>();
            if (appUser == null)
            {
                return NotFound();
            }

            foreach (var user in appUser)
            {
                if (!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    nonMembers.Add(user);
                }
            }
            return nonMembers;
        }

        /// <summary>
        /// Delete user from role
        /// </summary>
        /// <param name="appRole"></param>
        /// <returns></returns>
        [HttpPut("Remove/UsersFromRole")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicApi.DTO.v1.AppRoles))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> DeleteUsersFromRole(AppRole appRole)
        {
            var appUser = await _userManager.FindByIdAsync(appRole.Id.ToString());
            if (appUser == null)
            {
                return NotFound();
            }

            await _userManager.RemoveFromRoleAsync(appUser, appRole.Name);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Add user to role
        /// </summary>
        /// <param name="appRole"></param>
        /// <returns></returns>
        [HttpPut("AddUsersToRole")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicApi.DTO.v1.AppRoles))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> AddToRole(AppRole appRole)
        {
            var appUser = await _userManager.FindByIdAsync(appRole.Id.ToString());
            if (appUser == null)
            {
                return NotFound();
            }

            await _userManager.AddToRoleAsync(appUser, appRole.Name);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool AppRoleExists(Guid id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
