using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Zip.Installment.Entities;
using Zip.InstallmentsService;

namespace Zip.APIClient.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]
    public class PaymentPlanController : ControllerBase
    {
		private readonly ILogger<PaymentPlanController> _logger;
		private readonly IPaymentPlanFactory _paymentPlanFactory;
		public PaymentPlanController(ILogger<PaymentPlanController> logger, IPaymentPlanFactory paymentPlanFactory)
		{
			_paymentPlanFactory = paymentPlanFactory;
			_logger = logger;
		}


        /// <summary>
		/// This method is used to create the Payment Plan 
		/// this is post method and take Paymentpalnrequest as input
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(typeof(PaymentPlanResult), StatusCodes.Status201Created)]
		[HttpPost()]
		public async Task<IActionResult> CreatePaymentPlan([FromBody] PaymentPlanRequest request)
		{
			var result = await _paymentPlanFactory.CreatePaymentPlan(request);
			return CreatedAtAction(nameof(CreatePaymentPlan), result);
		}

		/// <summary>
		/// get the payment plan created, using Guid
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("/{id}")]
		[ProducesResponseType(typeof(PaymentPlanResult), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetPaymentPlan([FromRoute] Guid id)
		{
			var result = await _paymentPlanFactory.GetPaymentPlan(id);

			if (result == null)
				return NotFound();

			return Ok(result);
		}
	}
}