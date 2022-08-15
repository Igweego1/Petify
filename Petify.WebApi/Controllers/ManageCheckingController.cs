using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petify.Abstraction;
using Petify.Core.Model;
using Petify.Data.DBModels;
using System.Security.Claims;

namespace Petify.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageCheckingController : ControllerBase
    {
        private readonly ILogger<ManageCheckingController> _logger;
        private readonly ICheckingService _checkingService;

        public ManageCheckingController(ILogger<ManageCheckingController> logger, ICheckingService checkingService)
        {
            _logger = logger;
            _checkingService = checkingService;
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize]
        [HttpPost]
        [Route("CreateChecking")]

        public async Task<IActionResult> CreateChecking([FromBody] CheckingViewModel model)
        {
            _logger.LogInformation("User is Checking In...");
            if (!ModelState.IsValid)
            {
                _logger.LogCritical("Model state is Bad....");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Checking checking = new Checking();

            checking.StartDate = model.StartDate;
            checking.EndDate = model.EndDate;
            checking.ServiceId = model.ServiceId;
            //checking.BookingId = model.BookingId;
            checking.PetId = model.PetId;
            checking.Status = model.Status;
            checking.Created = DateTime.Now;
            checking.CreatedBy = userId;

            _checkingService.SaveChecking(checking);
            return Ok(model);
        }

        //Retrieve All  Checkings
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAllChecking")]

        public IActionResult GetAllChecking()
        {
            _logger.LogInformation("Getting List of All Checkings...");
            var result = _checkingService.GetCheckingList();
            return Ok(result);
        }

        //Get  Checkings by Id
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetChecking/{Id}")]

        public IActionResult GetChecking(int Id)
        {
            _logger.LogInformation("Getting Checking By Id...");
            var result = _checkingService.GetCheckingById(Id);
            return Ok(result);
        }
    }
}
