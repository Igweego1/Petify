using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Petify.Consume.Models;
using Petify.Core.Model;
using System.Net.Http.Headers;
using System.Text;

namespace Petify.Consume.Controllers
{
    public class ManagePetProfileController : Controller
    {
        private readonly IConfiguration _configuration;
        string apiUrl;

        public ManagePetProfileController(IConfiguration configuration)
        {
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("BaseUrl");
        }

        public async Task<IActionResult> Index()
        {

            PetProfileComponents petComponents = new PetProfileComponents();
            petComponents.profile = new PetProfileViewModel();
            string endpoint = apiUrl + "ManagePetProfile/RetrieveProfiles";

            List<PetTypeViewModel> petTypes = new List<PetTypeViewModel>()
            {
                new PetTypeViewModel{Id = 1, Name = "Dog"},
                new PetTypeViewModel{Id = 2, Name = "Cat"}
            };
            ViewBag.PetType = petTypes.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Name,
            }).ToList();
            petTypes.Insert(0, new PetTypeViewModel { Id = 0, Name = "Select Pet" });


            List<PetGenderViewModel> petGender = new List<PetGenderViewModel>()
            {
                new PetGenderViewModel { Id = 1, Name = "Male"},
                new PetGenderViewModel {Id = 2, Name = "Female"}
            };
            ViewBag.Gender = petGender.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Name,
            }).ToList();
            petGender.Insert(0, new PetGenderViewModel { Id = 0, Name = " Select Gender" });


            var token = Request.Cookies["access_token"].ToString();

            string endpointAllergy = apiUrl + "ManageAllergies/RetrieveAllergies";

            List<PetAllergyViewModel> petAllergies = new List<PetAllergyViewModel>();

            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(endpointAllergy);
                client.DefaultRequestHeaders.Authorization =
               new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                var response = await client.GetAsync(endpointAllergy); //this get response from 
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    petAllergies = JsonConvert.DeserializeObject<List<PetAllergyViewModel>>(content);
                }
            }

            //List<PetAllergyViewModel> petAllergy = new List<PetAllergyViewModel>()
            //{
            //    new PetAllergyViewModel { Id = 2, Name ="Chocolate"},
            //    new PetAllergyViewModel { Id = 3, Name ="Onions and Garlic"},
            //    new PetAllergyViewModel {Id = 4, Name ="Grapes and Raisins"},
            //    new PetAllergyViewModel { Id = 5, Name ="Milk and Other Diary Products"},
            //    new PetAllergyViewModel { Id = 6, Name = "Nuts"},
            //     new PetAllergyViewModel { Id = 7, Name = "None"}
            //};

            ViewBag.Allergy = petAllergies.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.AllergyName,
            }).ToList();



            
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
                    petComponents.profiles = JsonConvert.DeserializeObject<List<PetProfileViewModel>>(content);
                }
            }
            return View(petComponents);

        }

        [HttpPost]
        public async Task<IActionResult> CreatePet([FromForm] PetProfileComponents model)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(model.profile), Encoding.UTF8, "application/json");
            string endpoint = apiUrl + "ManagePetProfile/CreatePetProfile";
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
