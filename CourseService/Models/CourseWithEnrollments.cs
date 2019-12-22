using System.Runtime.Serialization;

namespace CourseService.Models
{
    [DataContract]
    public class CourseWithEnrollments : Course
    {
        public CourseWithEnrollments() { }

        public CourseWithEnrollments(Course course) : base()
        {
            Id = course.Id;
            Name = course.Name;
            Enrollments = course.Enrollments;
        }

        private int? _numberOfEnrollments;

        [DataMember]
        public int NumberOfEnrollments
        {
            get
            {
                return _numberOfEnrollments ?? (Enrollments != null ? Enrollments.Count : 0);
            }
            set
            {
                _numberOfEnrollments = value;
            }
        }
    }
}
