using CourseService.Integration.Test.Helpers;
using CourseService.Models;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace CourseService.Integration.Test
{
    [Trait("Category", "Integration")]
    [Collection("DatabaseCollection")]
    public class CoursesTests
    {
        [Fact]
        public async Task WhenIAddCourse_ThenICanRetrieveIt()
        {
            var newCourse = new Course { Name = "Course 1" };

            int newId = await CoursesRequests.AddCourse(newCourse);
            var course = await CoursesRequests.GetCourse(newId);

            Assert.Equal(newCourse.Name, course.Name);
        }

        [Fact]
        public async Task WhenIDeleteACourse_ThenICantRetrieveItAnymore()
        {
            var newCourse = new Course { Name = "Course 1" };

            int newId = await CoursesRequests.AddCourse(newCourse);
            var course = await CoursesRequests.GetCourse(newId);

            Assert.NotNull(course);

            Assert.True(await CoursesRequests.DeleteCourse(newId));

            Assert.Null(await CoursesRequests.GetCourse(newId));
        }

        [Fact]
        public async Task WhenIEnrollAUserToACourseThatDoesntExist_ThenIGetAProperErrorMessage()
        {
            var nonExistingCourseId = 100000;
            var newUser1 = new User { Email = "my.user1@northpass.com" };
            int newId1 = await UsersRequests.AddUser(newUser1);

            var enrollmentResponse = await CoursesRequests.EnrollUserToACourse(newId1, nonExistingCourseId);

            Assert.False(enrollmentResponse.Item1);
            Assert.Equal($"Course with Id: {nonExistingCourseId} does not exist!", enrollmentResponse.Item2);
        }

        [Fact]
        public async Task WhenIEnrollNonExistingUserToACourse_ThenIGetAProperErrorMessage()
        {
            var nonExistingUserId = 100000;
            var newCourse = new Course { Name = "Course 1" };
            int newId = await CoursesRequests.AddCourse(newCourse);

            var enrollmentResponse = await CoursesRequests.EnrollUserToACourse(nonExistingUserId, newId);

            Assert.False(enrollmentResponse.Item1);
            Assert.Equal($"User with Id: {nonExistingUserId} does not exist!", enrollmentResponse.Item2);
        }

        [Fact]
        public async Task WhenIEnrollAUserToACourseHeIsAlreadyEnrolledTo_ThenIGetAProperErrorMessage()
        {
            var newUser = new User { Email = "my.user@northpass.com" };
            var course1 = new Course { Name = "Course 1" };

            int user1Id = await UsersRequests.AddUser(newUser);
            int course1Id = await CoursesRequests.AddCourse(course1);

            var enrollmentResponse1 = await CoursesRequests.EnrollUserToACourse(user1Id, course1Id);

            Assert.True(enrollmentResponse1.Item1);
            Assert.Equal($"User: {user1Id} successfully enrolled to course: {course1Id}!", enrollmentResponse1.Item2);

            var enrollmentResponse2 = await CoursesRequests.EnrollUserToACourse(user1Id, course1Id);

            Assert.False(enrollmentResponse2.Item1);
            Assert.Equal("User already enrolled to that course!", enrollmentResponse2.Item2);
        }

        [Fact]
        public async Task WhenIWithdrawAUserFromACourseThatDoesntExist_ThenIGetAProperErrorMessage()
        {
            var nonExistingCourseId = 100000;
            var newUser1 = new User { Email = "my.user1@northpass.com" };
            int newId1 = await UsersRequests.AddUser(newUser1);

            var enrollmentResponse = await CoursesRequests.WithdrawUserFromACourse(newId1, nonExistingCourseId);

            Assert.False(enrollmentResponse.Item1);
            Assert.Equal($"Course with Id: {nonExistingCourseId} does not exist!", enrollmentResponse.Item2);
        }

        [Fact]
        public async Task WhenIWithdrawNonExistingUserFromACourse_ThenIGetAProperErrorMessage()
        {
            var nonExistingUserId = 100000;
            var newCourse = new Course { Name = "Course 1" };
            int newId = await CoursesRequests.AddCourse(newCourse);

            var enrollmentResponse = await CoursesRequests.WithdrawUserFromACourse(nonExistingUserId, newId);

            Assert.False(enrollmentResponse.Item1);
            Assert.Equal($"User with Id: {nonExistingUserId} does not exist!", enrollmentResponse.Item2);
        }

        [Fact]
        public async Task WhenIWithdrawAUserFromACourseHeWasntEnrolledTo_ThenIGetAProperErrorMessage()
        {
            var newUser = new User { Email = "my.user@northpass.com" };
            var course1 = new Course { Name = "Course 1" };

            int user1Id = await UsersRequests.AddUser(newUser);
            int course1Id = await CoursesRequests.AddCourse(course1);

            var enrollmentResponse1 = await CoursesRequests.WithdrawUserFromACourse(user1Id, course1Id);

            Assert.False(enrollmentResponse1.Item1);
            Assert.Equal("User was not enrolled to that course!", enrollmentResponse1.Item2);
        }

        [Fact]
        public async Task WhenIWithdrawAUserFromACourse_ThenIGetASuccessMessage()
        {
            var newUser = new User { Email = "my.user@northpass.com" };
            var course1 = new Course { Name = "Course 1" };

            int user1Id = await UsersRequests.AddUser(newUser);
            int course1Id = await CoursesRequests.AddCourse(course1);

            var enrollmentResponse1 = await CoursesRequests.EnrollUserToACourse(user1Id, course1Id);

            Assert.True(enrollmentResponse1.Item1);
            Assert.Equal($"User: {user1Id} successfully enrolled to course: {course1Id}!", enrollmentResponse1.Item2);

            var enrollmentResponse2 = await CoursesRequests.WithdrawUserFromACourse(user1Id, course1Id);

            Assert.True(enrollmentResponse2.Item1);
            Assert.Equal($"User: {user1Id} successfully withdrawed from course: {course1Id}!", enrollmentResponse2.Item2);
        }

        [Fact]
        public async Task WhenIEnrollUsersToCourses_ThenICanRetrieveTheListOfCoursesWithEnrollments()
        {
            var newUser1 = new User { Email = "my.user1@northpass.com" };
            var newUser2 = new User { Email = "my.user2@northpass.com" };
            var newUser3 = new User { Email = "my.user3@northpass.com" };
            var course1 = new Course { Name = "Course 1" };
            var course2 = new Course { Name = "Course 2" };
            var course3 = new Course { Name = "Course 3" };

            int newId1 = await UsersRequests.AddUser(newUser1);
            int newId2 = await UsersRequests.AddUser(newUser2);
            int newId3 = await UsersRequests.AddUser(newUser3);
            int course1Id = await CoursesRequests.AddCourse(course1);
            int course2Id = await CoursesRequests.AddCourse(course2);
            int course3Id = await CoursesRequests.AddCourse(course3);

            Assert.True((await CoursesRequests.EnrollUserToACourse(newId1, course1Id)).Item1);
            Assert.True((await CoursesRequests.EnrollUserToACourse(newId1, course2Id)).Item1);
            Assert.True((await CoursesRequests.EnrollUserToACourse(newId2, course2Id)).Item1);
            Assert.True((await CoursesRequests.EnrollUserToACourse(newId2, course3Id)).Item1);
            Assert.True((await CoursesRequests.EnrollUserToACourse(newId3, course1Id)).Item1);
            Assert.True((await CoursesRequests.EnrollUserToACourse(newId3, course2Id)).Item1);

            var coursesWithEnrollments = await CoursesRequests.GetCoursesWithEnrollments();

            Assert.Equal(2, coursesWithEnrollments.Single(x => x.Id.Equals(course1Id)).NumberOfEnrollments);
            Assert.Equal(3, coursesWithEnrollments.Single(x => x.Id.Equals(course2Id)).NumberOfEnrollments);
            Assert.Equal(1, coursesWithEnrollments.Single(x => x.Id.Equals(course3Id)).NumberOfEnrollments);
        }
    }
}
