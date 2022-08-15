using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Petify.Abstraction;
using Petify.Data.DBModels;
using Petify.WebApi.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Petify.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumanAccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HumanAccountController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly PetifyLiveContext _db;


        public HumanAccountController(IConfiguration configuration, ILogger<HumanAccountController> logger,
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            PetifyLiveContext db)
        {
            _configuration = configuration;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;

        }


        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] HumanViewModel human)
        {
            _logger.LogInformation("Registering User......");
            if (!ModelState.IsValid)
            {
                _logger.LogCritical("Model state is bad, check your model");
                return BadRequest(ModelState);
            }


            var roleExist = await _roleManager.RoleExistsAsync("Customer");

            if (!roleExist)
            {
                var role = new IdentityRole();
                role.NormalizedName = "Customer";
                role.Name = "Customer";
                await _roleManager.CreateAsync(role);
            }

            var user = new ApplicationUser();
            user.FirstName = human.FirstName;
            user.LastName = human.LastName;
            user.UserName = human.UserName;
            user.PhoneNumber = human.PhoneNumber;
            user.Email = human.Email;
            user.Created = DateTime.Now;

            var result = await _userManager.CreateAsync(user, human.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                return Ok(result);
            }

            return BadRequest(ModelState);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Register/{roleName}")]
        public async Task<IActionResult> Register([FromBody] HumanViewModel human, string roleName)
        {
            _logger.LogInformation("Registering a User as an Admin......");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var role = new IdentityRole();
                role.NormalizedName = roleName;
                role.Name = roleName;
                await _roleManager.CreateAsync(role);
            }

            var user = new ApplicationUser();
            user.FirstName = human.FirstName;
            user.LastName = human.LastName;
            user.UserName = human.UserName;
            user.PhoneNumber = human.PhoneNumber;
            user.Email = human.Email;
            user.Created = DateTime.Now;

            var result = await _userManager.CreateAsync(user, human.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, roleName);
                return Ok(result);
            }

            return BadRequest(ModelState);
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel human)
        {
            _logger.LogInformation("Login Was Successful!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var result = await _signInManager.PasswordSignInAsync(human.Username, human.Password,
                isPersistent: false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(human.Username);
                var stringtoken = GenerateAuthenticatedUserToken(user);
                return Ok(new {
                    firstName = user.FirstName, 
                    lastName = user.LastName,
                    userName = user.UserName,
                    UserId = user.Id,
                    email = user.Email,
                    token = stringtoken });
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("LogOut was Successful! See you Soon!");
            return Ok("Logout Successful");

        }

        //Retrieve All AspNetUsers


        [HttpGet]
        [Route("GetAllusers")]
        public List<AspNetUser> GetAllUser()
        {
            _logger.LogInformation("Getting all users...");
            return _db.AspNetUsers.ToList();
        }

        //public List<ApplicationUser> Users()
        //{
        //    return _userManager.Users.ToList();
        //    // return _db.AspNetUsers.ToList();
        //}



        //Retrieve Users By Id

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetByUserId")]
        public List<AspNetUser> GetUserById(int userId)
        {
            _logger.LogInformation("Getting all users by Id");
            if (userId != 0)
            {
                var result = _db.AspNetUsers.Where(x => x.UserId == userId).ToList();
                return result;

            }
            else
            {
                return null;
            }

        }



        private string GenerateAuthenticatedUserToken(ApplicationUser user)
        {
            var signKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));

            var credential = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);
            var claim = new[]
            {
                new Claim(ClaimTypes.Name, user.FirstName +' '+ user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Role, "Customer"),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claim,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(1), credential);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }




    }
}
