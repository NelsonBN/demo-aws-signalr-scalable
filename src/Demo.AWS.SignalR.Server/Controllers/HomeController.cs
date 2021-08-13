using Demo.AWS.SignalR.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.AWS.SignalR.Server.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly IMetadataService _metadataService;

        public HomeController(IMetadataService metadataService)
        {
            this._metadataService = metadataService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = this._metadataService
                .GetMetadata();

            return this.Ok(response);
        }
    }

}