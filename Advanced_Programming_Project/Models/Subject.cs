using System.Collections.ObjectModel;

namespace Models
{
    public class Subject
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string? Name { get; set; }
        public int MinimumDegree { get; set; }
        public short Term { get; set; }
        public short Year { get; set; }
        public Collection<SubjectLecture> Lectures { get; internal set; }
        public Department Department { get; set; }
        public Collection<Exam> Exams { get;  set; }
    }
}
