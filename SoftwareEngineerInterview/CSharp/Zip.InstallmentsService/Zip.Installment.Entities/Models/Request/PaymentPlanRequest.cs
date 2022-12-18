
namespace Zip.Installment.Entities
{
    public class PaymentPlanRequest
	{       
        public decimal PurchaseAmount { get; set; }

        public int NoOfInstallments { get; set; }

        public int?  InstallmentFrequency { get; set; }

        public DateTime PurhcaseDate { get; set; }
	}
}
