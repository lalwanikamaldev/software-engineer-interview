using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zip.Installment.Entities.Business;

namespace Zip.InstallmentsService.Repository
{
	public class PaymentPlanRepository : EfCoreRepository<PaymentPlan, ZipDBContext>
	{
		public PaymentPlanRepository(ZipDBContext context) : base(context)
		{

		}
		// add method which are specific to this repo
	}
}
