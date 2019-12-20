using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CourseService.Models
{
    [DataContract]
    public class User
    {
        [Required]
        [DataMember]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [DataMember]
        public string Email { get; set; }

        [JsonIgnore]
        public List<Enrollment> Enrollments { get; set; }
    }
}
