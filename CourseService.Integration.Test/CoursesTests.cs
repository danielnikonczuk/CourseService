using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CourseService.Integration.Test
{
    [Trait("Category", "Integration")]
    public class CoursesTests
    {
        private HttpClient httpClient;

        public CoursesTests()
        {
        }
    }
}
