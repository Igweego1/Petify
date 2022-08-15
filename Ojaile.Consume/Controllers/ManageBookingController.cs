using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Petify.Consume.Models;
using Petify.Core.Model;
using System.Net.Http.Headers;
using System.Text;

namespace Petify.Consume.Controllers
{
    public class ManageBookingController : Controller
    {
        private readonly IConfiguration _configuration;
        string apiUrl;

        public ManageBookingController(IConfiguration configuration)
        {
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("BaseUrl");
        }

        public async Task<IActionResult> Index()
        {
            BookingComponents bookingComponents = new BookingComponents();
            bookingComponents.booking = new Core.Model.BookingViewModel();
            string endpoint = apiUrl + "ManageBooking/GetAllBookings";

            var token = Request.Cookies["access_token"].ToString();


            string endpointServices = apiUrl + "ManageServices/RetrieveServices";

            List<ServiceViewModel>  serviceViews = new List<ServiceViewModel>();

            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(endpointServices);
                client.DefaultRequestHeaders.Authorization =
               new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                var response = await client.GetAsync(endpointServices); //this get response from 
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    serviceViews = JsonConvert.DeserializeObject<List<ServiceViewModel>>(content);
                }
            }


            ViewBag.Service = serviceViews.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.ServiceName,
                
            }).ToList();


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
                    bookingComponents.bookings = JsonConvert.DeserializeObject<List<BookingViewModel>>(content);
                }
            }
            return View(bookingComponents);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromForm] BookingComponents model)
        {
            //model.booking.ServiceId = 3;
           // model.booking.BillingId = 3;

            StringContent content = new StringContent(JsonConvert.SerializeObject(model.booking), Encoding.UTF8, "application/json");
            string endpoint = apiUrl + "ManageBooking/RequestBookingService";

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
                    TempData["user"] = model.booking.CreatedBy;
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
