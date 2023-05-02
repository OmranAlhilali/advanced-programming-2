
using Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class EFContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectLecture> SubjectLectures { get; set; }
        public DbSet<StudentMark> StudentMarks { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Department> Departments { get; set; }


        public EFContext(DbContextOptions<EFContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubjectLecture>()
            .HasOne<Subject>(e => e.Subject)
            .WithMany(e => e.Lectures)
            .HasForeignKey(e => e.SubjectId);

            modelBuilder.Entity<Subject>()
            .HasOne<Department>(e => e.Department)
            .WithMany(e => e.Subjects)
            .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Student>()
            .HasOne<Department>(e => e.Department)
            .WithMany(e => e.Students)
            .HasForeignKey(pam => pam.DepartmentId);

            modelBuilder.Entity<Exam>()
            .HasOne<Subject>(e => e.Subject)
            .WithMany(e => e.Exams)
            .HasForeignKey(pcm => pcm.SubjectId);

            //modelBuilder.Entity<StudentMark>()
            //.HasOne<Student>(e => e.Student)
            //.WithMany(e => e.Marks)
            //.HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<StudentMark>()
            .HasOne<Exam>(e => e.Exam)
            .WithMany(e => e.StudentMarks)
            .HasForeignKey(r => r.ExamId);

        }
    }
}
