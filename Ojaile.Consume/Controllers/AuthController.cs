using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Petify.Consume.Models;
using System.Text;

namespace Petify.Consume.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        string apiUrl;
        IHttpContextAccessor _httpContextAccessor;

        public AuthController(IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("BaseUrl");
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Login()
        {
            Models.LoginViewModel vm = new Models.LoginViewModel();
            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            string endpoint = apiUrl + "HumanAccount/Login";
            using (HttpClient client = new HttpClient())
            {
                var Response = await client.PostAsync(endpoint, content);



                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var response = await Response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LoginTokenise>(response);
                    var access_token = _httpContextAccessor.HttpContext.Request.Cookies["access_token"];

                    Set("access_token", result.token, 30);
                    Set("first_name", result.firstname, 30);
                    Set("last_name", result.lastname, 30);
                    Set("email", result.email, 30);
                    Set("user_name", result.username, 30);


                    //TempData["token"] = result;// JsonConvert.DeserializeObject<string>(result);
                    TempData["LoginSuccess"] = "Login Sucessfull !";
                    client.Dispose ();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Username or password incorrect");
                    return View();
                }
            }

        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            AuthModelComponents authModelComponents = new AuthModelComponents();
            authModelComponents.user = new RegisterViewModel();
            string endpoint = apiUrl + "HumanAccount/GetUsers";
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(endpoint);
                var content = await response.Content.ReadAsStringAsync();
                authModelComponents.register = JsonConvert.DeserializeObject<List<RegisterViewModel>>(content);

            }

            return View(authModelComponents);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] AuthModelComponents model)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(model.user), Encoding.UTF8, "application/json");
            string endpoint = apiUrl + "HumanAccount/Register"; ;
            using (HttpClient client = new HttpClient())
            {
                var Response = await client.PostAsync(endpoint, content);



                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //TempData["Profile"] = JsonConvert.SerializeObject(model);
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Username already exist");
                    return View();
                }
            }
        }

        public async Task<IActionResult> Logout()
        {
            Remove("access_token");
            Remove("first_name");
            Remove("last_name");
            Remove("user_name");
            return RedirectToAction("Login");
        }

        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions options = new CookieOptions();
            if (expireTime.HasValue)
                options.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                options.Expires = DateTime.Now.AddMinutes(10);
            Response.Cookies.Append(key, value, options);
        }

        public void Remove(string key)
        {
            Response.Cookies.Delete(key);
        }
    }
}
