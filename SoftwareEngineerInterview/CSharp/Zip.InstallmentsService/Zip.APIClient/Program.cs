using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Zip.APIClient;
using Zip.InstallmentsService;
using FluentValidation.AspNetCore;
using Zip.APIClient.Validators;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});

builder.Services.AddScoped<IPaymentPlanFactory, PaymentPlanFactory>();


// Add ApiExplorer to discover versions
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});


//builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddDbContext<ZipDBContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<PaymentPlanRequestValidator>());

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "My Rijsat API",
		Description = "Rijsat ASP.NET Core Web API",
		Contact = new OpenApiContact
		{
			Name = "Rijwan Ansari",
			Email = string.Empty,
		}

	});
});



var app = builder.Build();
// Register the Swagger generator, defining 1 or more Swagger documents




var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
