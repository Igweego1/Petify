using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petify.Abstraction;
using Petify.Data.DBModels;
using Petify.WebApi.Model;
using System.Security.Claims;


namespace Petify.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageServicesController : ControllerBase
    {
        private readonly IServiceItem _services;
        private readonly ILogger<ManageServicesController> _logger;
        public ManageServicesController(IServiceItem services, ILogger<ManageServicesController> logger)
        {
            _services = services;
            _logger = logger;
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("CreateService")]

        public async Task<IActionResult> CreateService([FromBody] ServiceViewModel model)
        {
            _logger.LogInformation("Creating A Service...");
            if (!ModelState.IsValid)
            {
                _logger.LogCritical("Model state is bad...");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Service service = new Service();

            service.ServiceName = model.ServiceName;

            _services.SaveService(service);
            return Ok(model);
        }


        //Retrieve All Services

        [Authorize(Roles ="Admin")]
        [HttpGet]
        [Route("RetrieveServices")]

        public IActionResult RetrieveServices()
        {
            _logger.LogInformation("Getting List of Services...");
            var result = _services.GetListServices();
            return Ok(result);
        }


        //Get Service by Id
        [Authorize]
        [HttpGet]
        [Route("GetService/{Id}")]

        public IActionResult GetService(int Id)
        {
            _logger.LogInformation("Getting Services by Id...");
            var result = _services.GetServiceById(Id);
            return Ok(result);
        }

        //Update Service
        [Authorize(Roles ="Admin")]
        [HttpPut]
        [Route("UpdateServiceRender/{Id}")]

        public async Task<IActionResult> UpdateServiceRender([FromBody] ServiceViewModel model, int Id)
        {
            _logger.LogInformation("Updating Services...");
            if (!ModelState.IsValid)
            {
                _logger.LogCritical("Model state is bad....");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _services.UpdateService(Id, new Service
            {
                ServiceName = model.ServiceName,
            });
            return Ok(model);
        }



        //Delete Service By Id

        [Authorize]
        [HttpDelete]
        [Route("DeleteService/{Id}")]
        public async Task<IActionResult> DeleteService(int Id)
        {
            _logger.LogInformation("Deleting A Service...");
            _services.DeleteService(Id);
            return Ok();

        }

    }
}
