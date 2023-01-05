using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Zip.Installment.Entities;

namespace Zip.InstallmentsService.Test
{
    public class PaymentPlanControllerIntegrationTests
    {
        private readonly HttpClient _client;
        public PaymentPlanControllerIntegrationTests(TestingWebAppFactory<Program> factory)
            => _client = factory.CreateClient();

        [Fact]
        public async Task Index_WhenCalled_ReturnsPaymentPlan()
        {
            var response = await _client.GetAsync("/6");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("payment", responseString);
            Assert.Contains("payment2", responseString);
        }


        [Fact]
        public async Task Index_WhenCalled_CreatePaymentPlan()
        {
            var data = new PaymentPlanRequest();
            JsonContent content = JsonContent.Create(data);
            var response = await _client.PostAsync("/CreatePaymentPlan", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("payment", responseString);
            Assert.Contains("payment2", responseString);
        }
    }
}
