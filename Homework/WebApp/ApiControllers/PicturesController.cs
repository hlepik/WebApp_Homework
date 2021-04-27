using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;


namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Pictures
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class PicturesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PictureMapper _mapper = new PictureMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PicturesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all pictures
        /// </summary>
        /// <returns>Entities from db</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Picture), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Picture>>> GetPictures()
        {
            return Ok((await _bll.Picture
                .GetAllPicturesAsync(User.GetUserId()!.Value)).Select(s => new PublicApi.DTO.v1.Picture()
            {
                Id = s.Id,
                Url = s.Url,
                ProductId = s.ProductId,
                ProductName = s.ProductName
            }));

        }

        /// <summary>
        /// Get one picture. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>Picture entity from db</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Picture), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<ActionResult<PublicApi.DTO.v1.Picture>> GetPicture(Guid id)
        {
            var picture = await _bll.Picture.FirstOrDefaultDTOAsync(id, User.GetUserId()!.Value);
            if (picture == null)
            {
                return NotFound(new Message("Picture not found"));
            }

            return Ok(_mapper.Map(picture));
        }

        /// <summary>
        /// Update picture
        /// </summary>
        /// <param name="id"></param>
        /// <param name="picture"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Message))]


        public async Task<IActionResult> PutPicture(Guid id, PublicApi.DTO.v1.Picture picture)
        {
            if (id != picture.Id)
            {
                return NotFound(new Message("Id and picture.id do not match"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new Message("Fields can't be empty!"));
            }

            _bll.Picture.Update(_mapper.Map(picture));
            await _bll.SaveChangesAsync();
            return NoContent();

        }

        /// <summary>
        /// Post picture
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Picture))]
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.v1.Picture>> PostPicture(PublicApi.DTO.v1.PictureAdd picture)
        {

            var bllPicture = new BLL.App.DTO.Picture()
            {
                Url = picture.Url,
                ProductId = picture.ProductId,
            };

            var addedPicture = _bll.Picture.Add(bllPicture);

            await _bll.SaveChangesAsync();

            var returnPicture = new PublicApi.DTO.v1.Picture()
            {
                Id = addedPicture.Id,
                Url = addedPicture.Url,
                ProductId = addedPicture.ProductId,

            };

            return CreatedAtAction("GetPicture",
                new
                {
                    id = returnPicture.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, returnPicture);
        }

        /// <summary>
        /// Delete picture
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicApi.DTO.v1.Picture))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> DeletePicture(Guid id)
        {
            var picture = await _bll.Picture.FirstOrDefaultAsync(id);
            if (picture == null)
            {
                return NotFound(new Message("Picture not found"));
            }

            _bll.Picture.Remove(picture);
            await _bll.SaveChangesAsync();

            return Ok(picture);
        }

    }
}
