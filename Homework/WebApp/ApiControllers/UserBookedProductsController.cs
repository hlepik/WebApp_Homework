using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using UserBookedProducts = Domain.App.UserBookedProducts;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UserBookedProductsController : ControllerBase
    {

        private readonly IAppBLL _bll;

        public UserBookedProductsController(IAppBLL bll)
        {

            _bll = bll;
        }

        // GET: api/UserBookedProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BLL.App.DTO.UserBookedProducts>>> GetUserBookedProducts()
        {
            return Ok(await _bll.UserBookedProducts.GetAllBookedProductsAsync(User.GetUserId()!.Value));
        }

        // GET: api/UserBookedProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BLL.App.DTO.UserBookedProducts>> GetUserBookedProducts(Guid id)
        {
            var userBookedProducts = await _bll.UserBookedProducts.FirstOrDefaultBookedProductsAsync(id);

            if (userBookedProducts == null)
            {
                return NotFound();
            }

            return userBookedProducts;
        }

        // PUT: api/UserBookedProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserBookedProducts(Guid id, BLL.App.DTO.UserBookedProducts userBookedProducts)
        {
            if (id != userBookedProducts.Id)
            {
                return BadRequest();
            }

            _bll.UserBookedProducts.Update(userBookedProducts);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/UserBookedProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserBookedProducts>> PostUserBookedProducts(BLL.App.DTO.UserBookedProducts userBookedProducts)
        {
            _bll.UserBookedProducts.Add(userBookedProducts);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserBookedProducts", new { id = userBookedProducts.Id }, userBookedProducts);
        }

        // DELETE: api/UserBookedProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserBookedProducts(Guid id)
        {
            var userBookedProducts = await _bll.UserBookedProducts.FirstOrDefaultBookedProductsAsync(id, User.GetUserId()!.Value);
            if (userBookedProducts == null)
            {
                return NotFound();
            }

            _bll.UserBookedProducts.Remove(userBookedProducts);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
