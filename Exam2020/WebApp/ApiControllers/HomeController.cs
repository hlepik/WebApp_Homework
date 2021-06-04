using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.App.DTO;
using DAL.App.EF;
using DAL.App.EF.Mappers;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebApp.ApiControllers

{

    /// <summary>
    /// API controller for Home
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HomeController
        : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uow"></param>
        public HomeController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }





    }
}