using CourseService.Integration.Test.Helpers;
using CourseService.Models;
using System.Threading.Tasks;
using Xunit;

namespace CourseService.Integration.Test
{
    [Trait("Category", "Integration")]
    [Collection("DatabaseCollection")]
    public class UsersTests
    {
        [Fact]
        public async Task WhenIAddUser_ThenICanRetrieveIt()
        {
            var newUserEmail = "my.user@northpass.com";

            var newUser = new User { Email = newUserEmail };

            int newId = await UsersRequests.AddUser(newUser);
            var user = await UsersRequests.GetUser(newId);

            Assert.Equal(newUserEmail, user.Email);
        }

        [Fact]
        public async Task WhenIDeleteAUser_ThenICantRetrieveItAnymore()
        {
            var newUser = new User { Email = "my.user@northpass.com" };

            int newId = await UsersRequests.AddUser(newUser);
            var user = await UsersRequests.GetUser(newId);

            Assert.NotNull(user);

            Assert.True(await UsersRequests.DeleteUser(newId));

            Assert.Null(await UsersRequests.GetUser(newId));
        }

        [Fact]
        public async Task WhenIEnrollUserToThreeCourses_ThenICanRetrieveTheListOfThem()
        {
            var newUser = new User { Email = "my.user@northpass.com" };
            var course1 = new Course { Name = "Course 1" };
            var course2 = new Course { Name = "Course 2" };
            var course3 = new Course { Name = "Course 3" };

            int newId = await UsersRequests.AddUser(newUser);
            int course1Id = await CoursesRequests.AddCourse(course1);
            int course2Id = await CoursesRequests.AddCourse(course2);
            int course3Id = await CoursesRequests.AddCourse(course3);

            var enrollmentResult1 = await CoursesRequests.EnrollUserToACourse(newId, course1Id);
            var enrollmentResult2 = await CoursesRequests.EnrollUserToACourse(newId, course2Id);
            var enrollmentResult3 = await CoursesRequests.EnrollUserToACourse(newId, course3Id);

            Assert.True(enrollmentResult1.Item1);
            Assert.True(enrollmentResult2.Item1);
            Assert.True(enrollmentResult3.Item1);

            var coursesForUser = await UsersRequests.GetUserCourses(newId);

            Assert.Collection(coursesForUser,
                             item => Assert.Contains(course1.Name, item.Name),
                             item => Assert.Contains(course2.Name, item.Name),
                             item => Assert.Contains(course3.Name, item.Name));
        }

    }
}
