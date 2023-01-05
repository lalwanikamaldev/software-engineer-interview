using FluentValidation;
using Zip.Installment.Entities;
using Zip.Installment.Entities.Business;

namespace Zip.APIClient.Validators
{
	public class PaymentPlanRequestValidator : AbstractValidator<PaymentPlanRequest> 
	{
		public PaymentPlanRequestValidator()
		{
			RuleFor(x => x.PurchaseAmount).GreaterThan(0);
		}
	}
}
