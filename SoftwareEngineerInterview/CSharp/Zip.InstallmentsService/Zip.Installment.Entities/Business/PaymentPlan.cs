using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zip.Installment.Entities.Business
{

	public class PaymentPlan
	{

		public const int INSTALLMENT_FREQUENCY = 14;
		public Guid Id { get; set; } = Guid.NewGuid();

		public decimal PurchaseAmount { get; set; }

		public IEnumerable<Installment> Installments { get; set; }

		public PaymentPlan()
		{
			Id = Guid.NewGuid();
			Installments = Enumerable.Empty<Installment>().ToList();
		}

		public PaymentPlan CreateInstallments(
			DateTime purchaseDate,
			decimal purchaseAmount,
			int installmentCount,
			int? installmentFrequency)
		{

			if (installmentCount == 0 || purchaseAmount == 0)
				return this;

			decimal installmentAmount = purchaseAmount / installmentCount;

			Installments = Enumerable.Range(0, installmentCount).Select(x => new Installment()
			{
				Amount = installmentAmount,
				DueDate = purchaseDate.AddDays(x * installmentFrequency ?? INSTALLMENT_FREQUENCY),
				Id = Id,
			}).ToList();

			return this;
		}
	}
}
