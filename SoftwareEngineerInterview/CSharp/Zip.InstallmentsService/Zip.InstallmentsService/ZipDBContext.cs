using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Zip.Installment.Entities.Business;

namespace Zip.InstallmentsService
{
	public class ZipDBContext : DbContext
	{

		protected readonly IConfiguration Configuration;

		public ZipDBContext(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			// connect to sql server with connection string from app settings
			options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
		}

		public DbSet<PaymentPlan> PaymentPlans { get; set; }
		public DbSet<Installment> Installments { get; set; }
	}
	
}
