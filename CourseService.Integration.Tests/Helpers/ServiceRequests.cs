using System;
using System.Net.Http;
using System.Text.Json;

namespace CourseService.Integration.Test.Helpers
{
    public class ServiceRequests
    {
        protected static readonly string ServiceUrl;
        protected static readonly HttpClient HttpClient = new HttpClient();
        protected static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        static ServiceRequests()
        {
            ServiceUrl = Environment.GetEnvironmentVariable("API_URL") ?? "http://localhost:8080";
        }
    }
}
