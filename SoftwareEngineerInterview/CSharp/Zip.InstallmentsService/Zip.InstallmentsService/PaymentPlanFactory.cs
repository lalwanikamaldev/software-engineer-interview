using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zip.Installment.Entities;
using Zip.Installment.Entities.Business;

namespace Zip.InstallmentsService
{
	public class PaymentPlanFactory : IPaymentPlanFactory
	{

		private readonly ZipDBContext _context;
		private readonly ILogger<PaymentPlanFactory> _logger;


		public PaymentPlanFactory(ZipDBContext context, ILogger<PaymentPlanFactory> logger)
		{
			_context = context;
			_logger = logger;

		}
		public async Task<PaymentPlan> CreatePaymentPlan(PaymentPlanRequest request)
		{
			_logger.LogInformation("Creating payment plan");
			try
			{

		
			PaymentPlan paymentPlan = new()
			{
				PurchaseAmount = request.PurchaseAmount
			};


			paymentPlan.CreateInstallments(request.PurhcaseDate, request.PurchaseAmount, request.NoOfInstallments, request.InstallmentFrequency);

			await _context.PaymentPlans.AddAsync(paymentPlan);

			await _context.SaveChangesAsync();

			_logger.LogInformation("payment plan created");

			return new PaymentPlan { Installments = paymentPlan.Installments, PurchaseAmount = paymentPlan.PurchaseAmount };
			}
			catch (Exception ex)
			{

				_logger.LogError($"error occoured while Creating plan . Detail are {ex}");
			}
			return null;
		}

		public async Task<PaymentPlan> GetPaymentPlan(Guid id)
		{
			_logger.LogInformation("get payment plan");
			var paymentPlan = await _context.PaymentPlans
		   .AsNoTracking()
		   .Include(y => y.Installments)
		.AsNoTracking()
		   .FirstOrDefaultAsync(x => x.Id == id);

			_logger.LogInformation("Fetched payment plan");
			return paymentPlan;
		}
	}
}
