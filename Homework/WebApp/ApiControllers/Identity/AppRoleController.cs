using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Identity;

namespace WebApp.ApiControllers.Identity
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
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        /// <summary>
        ///
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public AppRoleController( UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IAppBLL bll)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _bll = bll;
        }


        /// <summary>
        /// Returns all roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppRole>>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }


        /// <summary>
        /// return app role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Change app role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appRole"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppRole(Guid id, AppRole appRole)
        {
            if (id != appRole.Id)
            {
                return BadRequest();
            }

            await _roleManager.UpdateAsync(appRole);

            try
            {
                await _bll.SaveChangesAsync();
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


        /// <summary>
        /// adds new role
        /// </summary>
        /// <param name="appRole"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AppRole>> PostAppRole(AppRole appRole)
        {
            await _roleManager.CreateAsync(appRole);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetAppRole", new { id = appRole.Id }, appRole);
        }


        /// <summary>
        /// delete app role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppRole(Guid id)
        {
            var appRole = await _roleManager.FindByIdAsync(id.ToString());
            if (appRole == null)
            {
                return NotFound();
            }

            await _roleManager.DeleteAsync(appRole);
            await _bll.SaveChangesAsync();

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
            var appUser = await _userManager.Users.ToListAsync();
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
            var appUser = await _userManager.Users.ToListAsync();
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
            await _bll.SaveChangesAsync();

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
            await _bll.SaveChangesAsync();

            return NoContent();
        }


        private bool AppRoleExists(Guid id)
        {
            return _roleManager.Roles.Any(e => e.Id == id);
        }
    }
}
