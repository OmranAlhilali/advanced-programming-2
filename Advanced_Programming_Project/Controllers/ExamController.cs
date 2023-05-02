using Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;

namespace Controllers
{
    [ApiController]
    [Route("/Exams")]
    public class ExamsController : ControllerBase
    {
        readonly ExamRepository _Repo;
        readonly StudentRepository _StudentRepo;
        readonly StudentMarkRepository _StuMarkRepo;
        readonly EFContext context;

        public ExamsController(ExamRepository Repo,StudentMarkRepository StuMarkRepo, StudentRepository StudentRepo, EFContext context)
        {
            _Repo = Repo;
            _StuMarkRepo = StuMarkRepo;
            _StudentRepo = StudentRepo;
            this.context = context;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Exam>> GetExams(int page = 1, int count = 10)
        {
            if (page <= 0 || count <= 0)
            {
                return BadRequest("Page or count is less or equal to 0");
            }

            return Ok(_Repo.GetAll(count, page));
        }


        [HttpGet("{id}")]
        public ActionResult<Exam> GetExam(int id)
        {
            var Exam = _Repo.GetById(id);

            if (Exam == null)
            {
                return NotFound();
            }

            return Ok(Exam);
        }

        [HttpPost(nameof(AddExam))]
        public IActionResult AddExam([FromBody] ExamDto Exam)
        {

            _Repo.Add(new Exam { Term = Exam.Term,SubjectId=Exam.SubjectId });
            context.SaveChanges();
            //
            return StatusCode((int)System.Net.HttpStatusCode.Created);
        }

        [HttpPut(nameof(EditExam))]
        public IActionResult EditExam([FromBody] ExamDto Exam)
        {



            _Repo.Update(new Exam { Id = Exam.Id, Term = Exam.Term,SubjectId=Exam.SubjectId });
            context.SaveChanges();
            //
            return StatusCode((int)System.Net.HttpStatusCode.Created);
        }

        [HttpDelete("DeleteExams/{id}")]
        public IActionResult DeleteExams(int id)
        {
            var result = _Repo.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            _Repo.Remove(result);
            context.SaveChanges();

            return Ok("Deleted Successfully");
        }

        [HttpGet("Student_Not_Take_Exam/{ExamId}")]
        public ActionResult<IEnumerable<Exam>> Student_Not_Take_Exam(int ExamId,int page = 1, int count = 10)
        {
            if (page <= 0 || count <= 0)
            {
                return BadRequest("Page or count is less or equal to 0");
            }
            var r=_StuMarkRepo.GetStudentsNotExam(ExamId, page, count).ToList();
            List<Student> students = new List<Student>();   
            for(int i=0;i<r.Count();i++){
                students.Add( _StudentRepo.GetStudentsNotTakeExam(r[i].StudentId));
            }

            return Ok(students);
        }

        [HttpGet("Student_Take_Exam/{ExamId}")]
        public ActionResult<IEnumerable<Exam>> Student_Take_Exam(int ExamId,int page = 1, int count = 10)
        {
            if (page <= 0 || count <= 0)
            {
                return BadRequest("Page or count is less or equal to 0");
            }
            var r = _StuMarkRepo.GetStudentsNotExam(ExamId, page, count).ToList();
            List<Student> students = new List<Student>();
            for (int i = 0; i < r.Count(); i++)
            {
                students.Add(_StudentRepo.GetById(r[i].StudentId));
            }

            return Ok(students);
        }
    }

    public record class ExamDto(int Id, short Term,int SubjectId);
}
