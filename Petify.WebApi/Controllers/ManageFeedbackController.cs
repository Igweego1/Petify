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
    public class ManageFeedbackController : ControllerBase
    {
        private readonly IFeedBackItem _feedBack;
        private readonly ILogger<ManageFeedbackController> _logger;

        public ManageFeedbackController(IFeedBackItem feedBack, ILogger<ManageFeedbackController> logger)
        {
            _feedBack = feedBack;
            _logger = logger;

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize]
        [HttpPost]
        [Route("CreatingFeedback")]

        public async Task<IActionResult> CreatingFeedback([FromBody] FeedBackViewModel model)
        {
            _logger.LogInformation("User is Writing us a Feedback...");

            if (!ModelState.IsValid)
            {
                _logger.LogCritical("Model State is Bad...");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);



            FeedBack feedBack = new FeedBack();


            feedBack.Message = model.Message;
            feedBack.CreatedBy = userId;
            feedBack.Created = DateTime.Now;

            _feedBack.SaveFeedBack(feedBack);
            return Ok(model);
        }

        //Retrieve All FeedBacks
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
       [Authorize(Roles = "Admin")]
        [Route("RetrieveFeedBacks")]

        public IActionResult RetrieveFeedBacks()
        {
            _logger.LogInformation("Getting All Feedbacks...");
            var result = _feedBack.GetListFeedBack();
            return Ok(result);
        }

        //Get Feedbacks by Id
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetFeedBack/{Id}")]

        public IActionResult GetFeedBack(int Id)
        {
            _logger.LogInformation("Getting Feedbacks by Id...");
            var result = _feedBack.GetFeedBackById(Id);
            return Ok(result);
        }


        //Delete FeedBack By Id

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteFeedBack/{Id}")]
        public async Task<IActionResult> DeleteFeedBack(int Id)
        {
            _logger.LogInformation("Deleting feedback by Id...");
            _feedBack.DeleteFeedBack(Id);
            return Ok();
        }
    }
}
