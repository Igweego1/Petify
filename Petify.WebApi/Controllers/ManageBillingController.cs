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
    public class ManageBillingController : ControllerBase
    {
        private readonly ILogger<ManageBillingController> _logger;
        private readonly IBillingItem _billing;
        public ManageBillingController(ILogger<ManageBillingController> logger, IBillingItem billing)
        {
            _logger = logger;
            _billing = billing;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("CreateBillingPriceUnit")]
        public async Task<IActionResult> CreateBillingPriceunit([FromBody] BillingViewModel model)
        {
            _logger.LogInformation("Creating a Billing Price Unit...");
            if (!ModelState.IsValid)
            {
                _logger.LogCritical("Model State is bad....");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Billing billing = new Billing();
            billing.PriceUnit = model.PriceUnit;
            billing.CreatedBy = userId;
            billing.Created = DateTime.Now;


            _billing.SaveBillingPriceUnit(billing);

            return Ok(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetBilling/{Id}")]

        public IActionResult GetBillingById(int Id)
        {
            _logger.LogInformation("Getting Billing Price Unit by Id...");
            var result = _billing.GetBillingPriceunitById(Id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteBilling/{Id}")]
        public async Task<IActionResult> DeleteFeedback(int Id)
        {
            _logger.LogInformation("Deleting Billing Price Unit...");
            _billing.DeleteBillingPriceUnit(Id);
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
            billing.PriceUnit = model.PriceUnit;
            billing.Created = DateTime.Now;
            billing.CreatedBy = userId;


            _billing.UpdateBillingPriceUnit(Id, billing);
            return Ok(model);
        }
    }
}
