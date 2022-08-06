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
    public class ManagePaymentController : ControllerBase
    {
        private readonly ILogger<ManagePaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public ManagePaymentController(ILogger<ManagePaymentController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpPost]
        [Route("MakePayment")]

        public async Task<IActionResult> MakePayment([FromBody] PaymentViewModel model)
        {
            _logger.LogInformation("User is making a Payment...");
            if (!ModelState.IsValid)
            {
                _logger.LogCritical("Model state is Bad....");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Payment payment = new Payment();

            payment.ServiceId = model.ServiceId;
            payment.BillingId = model.BillingId;
            payment.UserId = userId;
            payment.TotalAmount = model.TotalAmount;
            payment.Created = DateTime.Now;
            payment.CreatedBy = userId;

            _paymentService.SavePayment(payment);
            return Ok(model);
        }

        //Retrieve All  Payments

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAllPayments")]

        public IActionResult GetAllPayments()
        {
            _logger.LogInformation("Getting List of All payments...");
            var result = _paymentService.GetPaymentList();
            return Ok(result);
        }

        //Get  Payments by Id
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetPayment/{Id}")]

        public IActionResult GetPayment(int Id)
        {
            _logger.LogInformation("Getting Payment By Id...");
            var result = _paymentService.GetPaymentById(Id);
            return Ok(result);
        }
    }
}
