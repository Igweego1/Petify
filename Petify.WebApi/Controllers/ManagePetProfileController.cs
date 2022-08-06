
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Petify.Abstraction;
using Petify.Core.Model;
using Petify.Data.DBModels;
using Petify.WebApi.Model;
using System.Security.Claims;

namespace Petify.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagePetProfileController : ControllerBase
    {
        private readonly IPetProfile _petProfile;
        private readonly ILogger<ManagePetProfileController> _logger;

        public ManagePetProfileController(IPetProfile petProfile, ILogger<ManagePetProfileController> logger)
        {
            _petProfile = petProfile;
            _logger = logger;
        }

        //To Create Pet Profile 

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("CreatePetProfile")]

        public async Task<IActionResult> CreatePetProfile([FromBody] PetProfileViewModel petModel)
        {
            _logger.LogInformation("Creating A Users Pet Profile...");
            if (!ModelState.IsValid)
            {
                _logger.LogCritical("Model state is Bad...");

                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Pet pet = new Pet();

            pet.PetName = petModel.PetName;
            pet.PetTypeId = petModel.PetTypeId;
            pet.PetGenderId = petModel.PetGenderId;
            pet.DateOfBirth = petModel.DateOfBirth;
            pet.AllergyId = petModel.AllergyId;
            pet.Description = petModel.Description;
            pet.PetAddress = petModel.PetAddress;
            pet.PetCity = petModel.PetCity;
            pet.CreatedBy = userId;
            pet.UserId = userId;
            pet.Created = DateTime.Now;
            pet.Image = petModel.Image;


            _petProfile.SavePetProfile(pet);
            return Ok(petModel);

        }


        //Retrieve All Pet Profiles

       [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("RetrieveProfiles")]

        public IActionResult RetrieveProfiles()
        {
            _logger.LogInformation("Getting List of All Pet profiles..");
            var result = _petProfile.GetPetProfile();
            return Ok(result);
        }


        //Get Pet Profile by Id
        [Authorize]
        [HttpGet]
        [Route("GetProfile/{Id}")]

        public IActionResult GetProfile(int Id)
        {
            _logger.LogInformation("Getting A Pet Profile By Id...");
            var result = _petProfile.GetPetProfileById(Id);
            return Ok(result);
        }

        //Update Pet Profile

        [Authorize]
        [HttpPut]
        [Route("UpdateProfile/{Id}")]

        public async Task<IActionResult> UpdateProfile([FromBody] PetProfileViewModel petModel, int Id)
        {
            _logger.LogInformation("Updating A pet Profile...");
            if (!ModelState.IsValid)
            {
                _logger.LogCritical("Model State is bad...");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Pet pet = new Pet();
            pet.PetName = petModel.PetName;
            pet.PetTypeId = petModel.PetTypeId;
            pet.PetGenderId = petModel.PetGenderId;
            pet.DateOfBirth = petModel.DateOfBirth;
            pet.AllergyId = petModel.AllergyId;
            pet.Description = petModel.Description;
            pet.PetAddress = petModel.PetAddress;
            pet.PetCity = petModel.PetCity;
            pet.CreatedBy = userId;
            pet.UserId = userId;
            pet.Created = DateTime.Now;
            pet.Image = petModel.Image;


            _petProfile.UpdatePetProfile(Id, pet);
            return Ok(pet);
        }



        //Delete Pet Profile By Id

        [Authorize(Roles ="Admin")]
        [HttpDelete]
        [Route("DeleteProfile/{Id}")]
        public async Task<IActionResult> DeleteProfile(int Id)
        {
            _logger.LogInformation("Deleting A pet Profile...");
            _petProfile.DeletePetProfile(Id);
            return Ok();
            

        }

       
    }

}
