using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petify.Abstraction;
using Petify.Core.Model;
using Petify.Data.DBModels;
//using Petify.WebApi.Model;
using System.Security.Claims;

namespace Petify.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageBookingController : ControllerBase
    {
        private readonly ILogger<ManageBookingController> _logger;
        private readonly IBookingService _bookingService;
        public ManageBookingController(ILogger<ManageBookingController> logger, IBookingService bookingService)
        {
            _logger = logger;
            _bookingService = bookingService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("RequestBookingService")]

        public async Task<IActionResult> RequestBookingService([FromBody] BookingViewModel model)
        {
            _logger.LogInformation("Booking for your pet to stay...");
            if (!ModelState.IsValid)
            {
                _logger.LogCritical("Model state is bad....");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Booking booking = new Booking();

            booking.CheckInDate = DateTime.Now;
            booking.CheckOutDate = DateTime.Now;
            booking.ServiceId = model.ServiceId;
            booking.BillingId = model.BillingId;
            booking.Amount = model.Amount;
            booking.UserId = userId;
            booking.Created = DateTime.Now;
            booking.CreatedBy = userId;

            _bookingService.SaveBooking(booking);
            return Ok(model);

        }

        //Retrieve All Bookings

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("GetAllBookings")]

        public IActionResult GetAllBookings()
        {
            _logger.LogInformation("Getting List of Bookings...");
            var result = _bookingService.GetBookingList();
            return Ok(result);
        }

        //Get Bookings by Id
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetBooking/{Id}")]

        public IActionResult GetBooking(int Id)
        {
            _logger.LogInformation("Getting Booking By Id...");
            var result = _bookingService.GetBookingById(Id);
            return Ok(result);
        }
    }
}
