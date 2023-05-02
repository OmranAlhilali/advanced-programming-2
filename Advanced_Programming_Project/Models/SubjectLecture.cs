namespace Models
{
    public class SubjectLecture
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Subject Subject { get; internal set; }
    }
}
