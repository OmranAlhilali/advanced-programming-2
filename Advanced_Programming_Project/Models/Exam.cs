using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace Models
{
    public class Exam
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int Date { get; set; }
        public short Term { get; set; }
        public Subject Subject { get;  set; }
        public Collection<StudentMark> StudentMarks { get; internal set; }
    }
}
