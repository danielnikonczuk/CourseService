using CourseService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseService.Integration.Test.Helpers
{
    public class CoursesRequests : ServiceRequests
    {
        public static async Task<IEnumerable<CourseWithEnrollments>> GetCoursesWithEnrollments()
        {
            using (var getResponse = await HttpClient.GetAsync($"{ServiceUrl}/api/courses"))
            {
                return JsonSerializer.Deserialize<IEnumerable<CourseWithEnrollments>>(await getResponse.Content.ReadAsStringAsync(), JsonSerializerOptions);
            };
        }

        public static async Task<Course> GetCourse(int newId)
        {
            using (var getResponse = await HttpClient.GetAsync($"{ServiceUrl}/api/courses/{newId}"))
            {
                if (getResponse.StatusCode == HttpStatusCode.NotFound)
                    return null;

                return JsonSerializer.Deserialize<Course>(await getResponse.Content.ReadAsStringAsync(), JsonSerializerOptions);

            };
        }

        public static async Task<int> AddCourse(Course newCourse)
        {
            using (var postResponse = await HttpClient.PostAsync($"{ServiceUrl}/api/courses",
                    new StringContent(JsonSerializer.Serialize(newCourse), Encoding.UTF8, "application/json")))
            {
                postResponse.EnsureSuccessStatusCode();
                var addedCourse = JsonSerializer.Deserialize<Course>(await postResponse.Content.ReadAsStringAsync(), JsonSerializerOptions);
                return addedCourse.Id;
            }
        }

        public static async Task<bool> DeleteCourse(int newId)
        {
            using (var deleteResponse = await HttpClient.DeleteAsync($"{ServiceUrl}/api/courses/{newId}"))
            {
                return deleteResponse.IsSuccessStatusCode;
            };
        }

        public static async Task<Tuple<bool, string>> EnrollUserToACourse(int userId, int courseId)
        {
            using (var putResponse = await HttpClient.PutAsync($"{ServiceUrl}/api/courses/enroll/{userId}/{courseId}", null))
            {
                string message = JsonDocument.Parse(await putResponse.Content.ReadAsStringAsync())
                    .RootElement.GetProperty("message").GetString();

                return new Tuple<bool, string>(putResponse.StatusCode == HttpStatusCode.OK, message);
            }
        }

        public static async Task<Tuple<bool, string>> WithdrawUserFromACourse(int userId, int courseId)
        {
            using (var deleteResponse = await HttpClient.DeleteAsync($"{ServiceUrl}/api/courses/withdraw/{userId}/{courseId}"))
            {
                string message = JsonDocument.Parse(await deleteResponse.Content.ReadAsStringAsync())
                    .RootElement.GetProperty("message").GetString();

                return new Tuple<bool, string>(deleteResponse.StatusCode == HttpStatusCode.OK, message);
            };
        }
    }
}
