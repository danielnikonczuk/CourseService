using CourseService.Integration.Test.Helpers;
using System.Threading.Tasks;
using Xunit;

namespace CourseService.Integration.Test
{
    [Trait("Category", "Integration")]
    public class HealthCheckTest
    {
        [Fact]
        public async Task HealthCheckTest_ReturnsHealthyStatus()
        {
            Assert.True(await HealthCheckRequests.GetHealth());
        }
    }
}
