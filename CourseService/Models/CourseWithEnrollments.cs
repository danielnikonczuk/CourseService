using System.Runtime.Serialization;

namespace CourseService.Models
{
    [DataContract]
    public class CourseWithEnrollments : Course
    {
        public CourseWithEnrollments(Course course) : base ()
        {
            Id = course.Id;
            Name = course.Name;
            Enrollments = course.Enrollments;
        }

        [DataMember]
        public int NumberOfEnrollments
        {
            get
            {
                return Enrollments != null ? Enrollments.Count : 0;
            }
        }
    }
}
