using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Petify.Consume.Models;
using Petify.Core.Model;
using System.Net.Http.Headers;
using System.Text;

namespace Petify.Consume.Controllers
{
    public class ManageCheckingController : Controller
    {
        private readonly IConfiguration _configuration;
        string apiUrl;

        public ManageCheckingController(IConfiguration configuration)
        {
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("BaseUrl");
        }

        public async Task<IActionResult> Index()
        {

            CheckingModelComponents  checkingComponents = new CheckingModelComponents();
            checkingComponents.checking = new CheckingViewModel();
            string endpoint = apiUrl + "ManageChecking/GetAllChecking";

            var token = Request.Cookies["access_token"].ToString();

            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };

            using (HttpClient client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Authorization =
               new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(endpoint); //this get response from 
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    checkingComponents.checkings = JsonConvert.DeserializeObject<List<CheckingViewModel>>(content);
                }
            }

            return View(checkingComponents);
        }


        [HttpPost]
        public async Task<IActionResult> AddChecking([FromForm] CheckingModelComponents model)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(model.checking), Encoding.UTF8, "application/json");
            string endpoint = apiUrl + "ManageChecking/CreateChecking";

            var token = Request.Cookies["access_token"].ToString();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var Response = await client.PostAsync(endpoint, content);



                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                   // TempData["user"] = model.booking.CreatedBy;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Username already exist");
                    return RedirectToAction("Index");
                }
            }
        }

    }
}
