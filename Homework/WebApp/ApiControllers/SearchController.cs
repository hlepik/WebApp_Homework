using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers

{

    /// <summary>
    /// API controller for Home
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SearchController
        : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ProductMapper _mapper = new ProductMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public SearchController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>Entities from db</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Product), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Product>>> GetProducts()
        {
            return Ok((await _bll.Product.GetAllProductsAsync())
                .Select(s => new PublicApi.DTO.v1.Product()
                {
                    Description = s.Description,
                    City = s.City,
                    County = s.County,
                    LocationDescription = s.LocationDescription,
                    AppUserId = s.AppUserId,
                    IsBooked = s.IsBooked,
                    DateAdded = s.DateAdded,
                    Color = s.Color,
                    Condition = s.Condition,
                    Material = s.Material!,
                    Width = s.Width,
                    Height = s.Height,
                    Depth = s.Depth,
                    HasTransport = s.HasTransport,
                    Unit = s.Unit,
                    Id = s.Id,
                    Category = s.Category,
                    PictureUrls = s.PictureUrls


                }));
        }


    }
}