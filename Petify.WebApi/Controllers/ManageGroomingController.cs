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
    public class ManageGroomingController : ControllerBase
    {
        private readonly PetifyLiveContext _db;
        private readonly ILogger<ManageGroomingController> _logger;
        private readonly IGroomingService _groomingService;
        public ManageGroomingController(IGroomingService groomingService, ILogger<ManageGroomingController> logger,
            PetifyLiveContext db)
        {
            _groomingService = groomingService;
            _logger = logger;
            _db = db;

        }

        [Authorize]
        [HttpPost]
        [Route("RequestGroomingService")]

        public async Task<IActionResult> RequestGroomingService([FromBody] GroomingViewModel model)
        {
            _logger.LogInformation("Requesting For A grooming Service...");
            if (!ModelState.IsValid)
            {
                _logger.LogCritical("Model state is Bad....");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Grooming grooming = new Grooming();

            grooming.ServiceId = model.ServiceId;
            grooming.BillingId = model.BillingId;
            grooming.UserId = userId;
            grooming.Created = DateTime.Now;
            grooming.CreatedBy = userId;

            _groomingService.SaveGrooming(grooming);
            return Ok(model);
        }


        //Retrieve All Groomers

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAllGrooming")]

        public IActionResult GetAllGrooming()
        {
            _logger.LogInformation("Getting List of Groomings...");
            var result = _groomingService.GetGroomingList();
            return Ok(result);
        }


        //Get Groomers by Id
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetGrooming/{Id}")]

        public IActionResult GetGrooming(int Id)
        {
            _logger.LogInformation("Getting A Grooming by Id...");
            var result = _groomingService.GetGroomingById(Id);
            return Ok(result);
        }
    }
}
