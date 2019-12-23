using System;
using System.Net;
using System.Threading.Tasks;

namespace CourseService.Integration.Test.Helpers
{
    public class HealthCheckRequests : ServiceRequests
    {
        public static async Task<bool> GetHealth()
        {
            var query = $"{ServiceUrl}/health";
            Console.WriteLine($"Querying address: {query}");

            using var response = await HttpClient.GetAsync($"{ServiceUrl}/health");

            return
                HttpStatusCode.OK == response.StatusCode &&
               "Healthy" == await response.Content.ReadAsStringAsync();
        }
    }
}
