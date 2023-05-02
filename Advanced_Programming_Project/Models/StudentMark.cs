using System.Text.Json.Serialization;

namespace Models
{
    public class StudentMark
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public int Mark { get; set; }
        [JsonIgnore]
        public Student Student { get;  set; }
        public Exam Exam { get;  set; }

    }
}
