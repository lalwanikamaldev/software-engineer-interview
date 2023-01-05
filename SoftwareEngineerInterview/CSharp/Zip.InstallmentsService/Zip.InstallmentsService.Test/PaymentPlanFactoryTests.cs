using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using Zip.Installment.Entities;
using System.Linq;

namespace Zip.InstallmentsService.Test
{
    public class PaymentPlanFactoryTests 
    {
        protected IServiceCollection _services;
		protected IServiceProvider _serviceProvider;

		public PaymentPlanFactoryTests()
        {
            RegisterDI();
        }

		protected void RegisterDI()
		{
			var configuration = new ConfigurationBuilder().Build();
			_services = new ServiceCollection();
			_services.AddTransient<IPaymentPlanFactory, PaymentPlanFactory>();
			 _serviceProvider = _services.BuildServiceProvider();
		}


	/// <summary>
	/// This will create sucess test cases 
	/// </summary>
	/// <param name="purchaseAmount"></param>
	/// <param name="installmentFrequency"></param>
	/// <param name="installmentAmount"></param>
	/// <returns></returns>
		[Theory]
		[InlineData(2000, 4, 500)]
		[InlineData(1850, 4, 462.50)]
		[InlineData(2888, 4, 722)]
		public async Task CreatePaymentPlanSuccessfully(decimal purchaseAmount, int installmentFrequency, decimal installmentAmount)
		{
			DateTime purchaseDate = new(2022, 01, 01);
			// Arrange
			var request = new PaymentPlanRequest
			{
				PurhcaseDate = purchaseDate,
				PurchaseAmount = purchaseAmount,
				InstallmentFrequency = installmentFrequency
			};
			
			var paymentPlan = _serviceProvider.GetService<IPaymentPlanFactory>();

			// Act
			 var paymentPlanResult = await paymentPlan.CreatePaymentPlan(request);

			//Assert
			Assert.NotNull(paymentPlanResult);
			Assert.Equal(paymentPlanResult?.PurchaseAmount, purchaseAmount);
			Assert.Equal(paymentPlanResult?.Installments.Count(), installmentFrequency);
			

			foreach (var installment in paymentPlanResult.Installments)
			{
				Assert.Equal(installment?.Amount, installmentAmount);
			}
		}


	}

}
