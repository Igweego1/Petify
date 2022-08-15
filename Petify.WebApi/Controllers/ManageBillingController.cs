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
    public class ManageBillingController : ControllerBase
    {
        private readonly ILogger<ManageBillingController> _logger;
        private readonly IBillingService _billing;
        public ManageBillingController(ILogger<ManageBillingController> logger, IBillingService billing)
        {
            _logger = logger;
            _billing = billing;
        }

        [Authorize]
        [HttpPost]
        [Route("CreateBilling")]
        public async Task<IActionResult> CreateBilling([FromBody] BillingViewModel model)
        {
            _logger.LogInformation("Creating Billing...");
            if (!ModelState.IsValid)
            {
                _logger.LogCritical("Model State is bad....");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Billing billing = new Billing();
            billing.Quantity = model.Quantity;
            billing.PriceUnit = model.PriceUnit;
            billing.Total = model.Total;
            billing.Description = model.Description;
            billing.CreatedBy = userId;
            billing.Created = DateTime.Now;
            billing.Status = model.Status;


            _billing.SaveBilling(billing);

            return Ok(model);
        }


        //Retrieve All Billings

        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAllBillings")]

        public IActionResult GetAllBillings()
        {
            _logger.LogInformation("Getting List of Billings...");
            var result = _billing.GetListBilling();
            return Ok(result);
        }



        [Authorize]
        [HttpGet]
        [Route("GetBilling/{Id}")]

        public IActionResult GetBillingById(int Id)
        {
            _logger.LogInformation("Getting Billing Price Unit by Id...");
            var result = _billing.GetBillingById(Id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteBilling/{Id}")]
        public async Task<IActionResult> DeleteBilling(int Id)
        {
            _logger.LogInformation("Deleting Billing Price Unit...");
            _billing.DeleteBilling(Id);
            return Ok();
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("UpdateBilling/{Id}")]
        public async Task<IActionResult> UpdateBillingItem([FromBody] BillingViewModel model, int Id)
        {
            _logger.LogInformation("Updating a Billing Price Unit...");
            if (!ModelState.IsValid)
            {
                _logger.LogCritical("Model State is bad....");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Billing billing = new Billing();
            billing.Quantity = model.Quantity;
            billing.PriceUnit = model.PriceUnit;
            billing.Total = model.Total;
            billing.Description = model.Description;
            billing.CreatedBy = userId;
            billing.Created = DateTime.Now;
            billing.Status = model.Status;

            _billing.UpdateBilling(Id, billing);
            return Ok(billing);
        }
    }
}
