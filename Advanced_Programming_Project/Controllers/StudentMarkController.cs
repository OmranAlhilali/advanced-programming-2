using Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;

namespace Controllers
{
    [ApiController]
    [Route("/StudentMarks")]
    public class StudentMarksController : ControllerBase
    {
        readonly StudentMarkRepository _Repo;
        readonly EFContext context;

        public StudentMarksController(StudentMarkRepository Repo, EFContext context)
        {
            _Repo = Repo;
            this.context = context;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<StudentMark>> GetStudentMarks(int page = 1, int count = 10)
        {
            if (page <= 0 || count <= 0)
            {
                return BadRequest("Page or count is less or equal to 0");
            }

            return Ok(_Repo.GetAll(count, page));
        }


        [HttpGet("{id}")]
        public ActionResult<StudentMark> GetStudentMark(int id)
        {
            var StudentMark = _Repo.GetById(id);

            if (StudentMark == null)
            {
                return NotFound();
            }

            return Ok(StudentMark);
        }

        [HttpPost(nameof(AddStudentMark))]
        public IActionResult AddStudentMark([FromBody] StudentMarkDto StudentMark)
        {

            _Repo.Add(new StudentMark { ExamId = StudentMark.ExamId, Mark = StudentMark.Mark,StudentId=StudentMark.StudentId });
            context.SaveChanges();
            
            return StatusCode((int)System.Net.HttpStatusCode.Created);
        }

        [HttpPut(nameof(EditStudentMark))]
        public IActionResult EditStudentMark([FromBody] StudentMarkDto StudentMark)
        {



            _Repo.Update(new StudentMark {Id=StudentMark.Id, ExamId = StudentMark.ExamId, Mark = StudentMark.Mark, StudentId = StudentMark.StudentId });
            context.SaveChanges();
            //
            return StatusCode((int)System.Net.HttpStatusCode.Created);
        }

        [HttpDelete("DeleteStudentMarks/{id}")]
        public IActionResult DeleteStudentMarks(int id)
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
    }

    public record class StudentMarkDto(int Id, int ExamId, int Mark,int StudentId);
}
