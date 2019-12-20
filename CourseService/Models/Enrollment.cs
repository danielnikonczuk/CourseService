using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CourseService.Models
{
    [DataContract]
    public class Enrollment
    {
        [Required]
        [DataMember]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [Required]
        [DataMember]
        public int CourseId { get; set; }

        [JsonIgnore]
        public Course Course { get; set; }
    }
}
