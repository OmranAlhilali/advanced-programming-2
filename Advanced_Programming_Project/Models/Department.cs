using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Collection<Subject> Subjects { get; set; }
        public Collection<Student> Students { get;  set; }
    }
}
