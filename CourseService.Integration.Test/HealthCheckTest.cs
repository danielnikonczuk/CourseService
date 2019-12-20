using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CourseService.Integration.Test
{
    [Trait("Category", "Integration")]
    public class HealthCheckTest
    {
        private HttpClient httpClient;

        public HealthCheckTest()
        {
        }

        [Fact]
        public async Task HealthCheckTest_ReturnsHealthyStatus()
        {

        }
    }
}
