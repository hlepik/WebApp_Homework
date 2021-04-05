using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Picture = Domain.App.Picture;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class PicturesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PicturesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Pictures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BLL.App.DTO.Picture>>> GetPictures()
        {
            return Ok(await _bll.Picture.GetAllPicturesAsync(User.GetUserId()!.Value));
        }

        // GET: api/Pictures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BLL.App.DTO.Picture>> GetPicture(Guid id)
        {
            var picture = await _bll.Picture.FirstOrDefaultAsync(id);

            if (picture == null)
            {
                return NotFound();
            }

            return picture;
        }

        // PUT: api/Pictures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPicture(Guid id, BLL.App.DTO.Picture picture)
        {
            if (id != picture.Id)
            {
                return BadRequest();
            }

            _bll.Picture.Update(picture);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Pictures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Picture>> PostPicture(BLL.App.DTO.Picture picture)
        {
            _bll.Picture.Add(picture);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPicture", new { id = picture.Id }, picture);
        }

        // DELETE: api/Pictures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(Guid id)
        {
            var picture = await _bll.Picture.FirstOrDefaultAsync(id);
            if (picture == null)
            {
                return NotFound();
            }

            _bll.Picture.Remove(picture);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
