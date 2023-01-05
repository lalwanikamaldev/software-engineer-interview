using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zip.Installment.Entities;
using Zip.Installment.Entities.Business;

namespace Zip.InstallmentsService
{
	public interface IPaymentPlanFactory
	{
		Task<PaymentPlan> CreatePaymentPlan(PaymentPlanRequest request);

		Task<PaymentPlan> GetPaymentPlan(Guid id);
	}
}
