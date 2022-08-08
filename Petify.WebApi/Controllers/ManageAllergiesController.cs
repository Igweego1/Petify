using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    public class ManageAllergiesController : ControllerBase
    {
        private readonly IAllergiesItem _allergies;
        private readonly ILogger<ManageAllergiesController> _logger;
        public ManageAllergiesController(IAllergiesItem allergies, ILogger<ManageAllergiesController> logger)
        {
            _allergies = allergies;
            _logger = logger;
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("CreateAllergy")]

        public async Task<IActionResult> CreateAllergy([FromBody] AllergiesViewModel model)
        {
            _logger.LogInformation("Admin is creating an allergy....");
            if (!ModelState.IsValid)
            {
                _logger.LogCritical("Model state is bad...");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Allergy allergy = new Allergy();

            allergy.AllergyName = model.AllergyName;

            _allergies.SaveAllergy(allergy);
            return Ok(model);

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Authorize(Roles ="Admin")]
        [HttpGet]
        [Route("RetrieveAllergies")]

        public IActionResult RetrieveAllergies()
        {
            _logger.LogInformation("Getting all Allergies....");
            var result = _allergies.GetListAllergy();
            return Ok(result);
        }


        //Get Allergy by Id
        [Authorize(Roles ="Admin")]
        [HttpGet]
        [Route("GetAllergy/{Id}")]

        public IActionResult GetAllergy(int Id)
        {
            _logger.LogInformation("Getting Allergy by Id");
            var result = _allergies.GetAllergyById(Id);
            return Ok(result);
        }


        //Update Allergy
        [Authorize(Roles ="Admin")]
        [HttpPut]
        [Route("UpdateAllergy/{Id}")]

        public async Task<IActionResult> UpdateAllergy([FromBody] AllergiesViewModel model, int Id)
        {
            _logger.LogInformation("Updating Allergy List....");
            if (!ModelState.IsValid)
            {
                _logger.LogCritical("Model state is Bad.... ");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _allergies.UpdateAllergy(Id, new Allergy
            {
                AllergyName = model.AllergyName,
            });
            return Ok(model);
        }


        //Delete Allergy By Id

        [Authorize(Roles ="Admin")]
        [HttpDelete]
        [Route("DeleteAllergy/{Id}")]
        public async Task<IActionResult> DeleteAllergy(int Id)
        {
            _logger.LogInformation("Allergy is being deleted...");
            _allergies.DeleteAllergy(Id);
            return Ok();

        }
    }
}
