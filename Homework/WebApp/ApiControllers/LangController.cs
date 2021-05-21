using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// LangController
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LangController : ControllerBase
    {
        private readonly ILogger<LangController> _logger;
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="localizationOptions"></param>
        public LangController(ILogger<LangController> logger, IOptions<RequestLocalizationOptions> localizationOptions)
        {
            _logger = logger;
            _localizationOptions = localizationOptions;
        }

        /// <summary>
        /// Returns all languages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  ActionResult<IEnumerable<SupportedLanguage>> GetSupportedLanguages()
        {
            var res = _localizationOptions.Value.SupportedUICultures.Select(c => new SupportedLanguage()
            {
                Name = c.Name,
                NativeName = c.NativeName,
            });
            return Ok(res);
        }

        /// <summary>
        /// Return lang resources
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  ActionResult<LangResources> GetLangResources()
        {
            return Ok(new LangResources());
        }


    }
}
