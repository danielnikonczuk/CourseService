using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CourseService.Integration.Test
{
    [Trait("Category", "Integration")]
    public class HealthCheckTest
    {
        private HttpClient _httpClient;

        public HealthCheckTest()
        {
            _httpClient = new HttpClient();
        }

        [Fact]
        public async Task HealthCheckTest_ReturnsHealthyStatus()
        {
            using var response = await _httpClient.GetAsync("/health");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(await response.Content.ReadAsStringAsync(), "Healthy");

        }
    }
}
