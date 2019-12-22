using System.Net.Http;
using System.Text.Json;

namespace CourseService.Integration.Test.Helpers
{
    public class ServiceRequests
    {
        protected const string ServiceUrl = "http://localhost:8080";
        protected static readonly HttpClient HttpClient = new HttpClient();
        protected static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
}
