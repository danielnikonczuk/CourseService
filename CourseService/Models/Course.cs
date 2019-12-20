using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CourseService.Models
{
    [DataContract]
    public class Course
    {
        [Required]
        [DataMember]
        public int Id { get; set; }

        [Required]
        [DataMember]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Enrollment> Enrollments { get; set; }
    }
}
