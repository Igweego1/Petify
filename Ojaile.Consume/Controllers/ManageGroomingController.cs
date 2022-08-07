using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Petify.Consume.Models;
using Petify.Core.Model;
using System.Net.Http.Headers;
using System.Text;

namespace Petify.Consume.Controllers
{
    public class ManageGroomingController : Controller
    {
        private readonly IConfiguration _configuration;
        string apiUrl;

        public ManageGroomingController(IConfiguration configuration)
        {
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("BaseUrl");
        }


        public async Task<IActionResult> Index()
        {
            GroomModelComponents groomModelComponents = new GroomModelComponents();
            groomModelComponents.grooming = new GroomViewModel();

            string endpoint = apiUrl + "ManageGrooming/GetAllGrooming";

            var token = Request.Cookies["access_token"].ToString();
            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
            };

            using (HttpClient client = new HttpClient(handler))
            {

                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Authorization =
               new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                var response = await client.GetAsync(endpoint); //this get response from 
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    groomModelComponents.groomings = JsonConvert.DeserializeObject<List<GroomViewModel>>(content);
                }
            }

            return View(groomModelComponents);
        }



        [HttpPost]
        public async Task<IActionResult> CreateGrooming([FromForm] GroomModelComponents model)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(model.grooming), Encoding.UTF8, "application/json");
            string endpoint = apiUrl + "ManageGrooming/RequestGroomingService";
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
                    //TempData["Profile"] = JsonConvert.SerializeObject(model);
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
