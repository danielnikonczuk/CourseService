using CourseService.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseService.Integration.Test.Helpers
{
    public class UsersRequests : ServiceRequests
    {
        public static async Task<User> GetUser(int userId)
        {
            using (var getResponse = await HttpClient.GetAsync($"{ServiceUrl}/api/users/{userId}"))
            {
                if (getResponse.StatusCode == HttpStatusCode.NotFound)
                    return null;

                return JsonSerializer.Deserialize<User>(await getResponse.Content.ReadAsStringAsync(), JsonSerializerOptions);

            };
        }

        public static async Task<int> AddUser(User newUser)
        {
            using (var postResponse = await HttpClient.PostAsync($"{ServiceUrl}/api/users",
                    new StringContent(JsonSerializer.Serialize(newUser), Encoding.UTF8, "application/json")))
            {
                postResponse.EnsureSuccessStatusCode();
                var addedUser = JsonSerializer.Deserialize<User>(await postResponse.Content.ReadAsStringAsync(), JsonSerializerOptions);
                return addedUser.Id;
            }
        }

        public static async Task<bool> DeleteUser(int userId)
        {
            using (var deleteResponse = await HttpClient.DeleteAsync($"{ServiceUrl}/api/users/{userId}"))
            {
                return deleteResponse.IsSuccessStatusCode;
            };
        }

        public static async Task<IEnumerable<Course>> GetUserCourses(int userId)
        {
            using (var getResponse = await HttpClient.GetAsync($"{ServiceUrl}/api/users/{userId}/courses"))
            {
                return JsonSerializer.Deserialize<IEnumerable<Course>>(await getResponse.Content.ReadAsStringAsync(), JsonSerializerOptions);
            };
        }
    }
}
