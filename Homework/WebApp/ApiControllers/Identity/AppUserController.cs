using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.Identity;

namespace WebApp.ApiControllers.Identity
{
    /// <summary>
    /// Api controller for AppUser
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppUserController : ControllerBase
    {
        private readonly IAppBLL _bll;

        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        ///
        /// </summary>
        /// <param name="userManager"></param>
        public AppUserController(UserManager<AppUser> userManager, IAppBLL bll)
        {

            _userManager = userManager;
            _bll = bll;
        }


        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }


        /// <summary>
        /// Retuns app user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetAppUser(Guid id)
        {
            var appUser = await _userManager.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            if (appUser == null)
            {
                return NotFound();
            }

            return appUser;
        }

        /// <summary>
        /// Edit user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appUser"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppUser(Guid id, AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return BadRequest();
            }

            await _userManager.UpdateAsync(appUser);
            await _bll.SaveChangesAsync();


            return NoContent();
        }


        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AppUser>> PostAppUser(AppUser appUser)
        {
            await _userManager.CreateAsync(appUser);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetAppUser", new { id = appUser.Id }, appUser);
        }


        /// <summary>
        /// delete app user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppUser(Guid id)
        {
            var appUser = await _userManager.FindByIdAsync(id.ToString());
            if (appUser == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(appUser);
            await _bll.SaveChangesAsync();

            return NoContent();
        }


    }
}
