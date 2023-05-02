using Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;

namespace Controllers
{
    [ApiController]
    [Route("/Students")]
    public class StudentsController : ControllerBase
    {
        readonly StudentRepository _Repo;
        readonly EFContext context;

        public StudentsController(StudentRepository Repo, EFContext context)
        {
            _Repo = Repo;
            this.context = context;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Student>> GetStudents(int page = 1, int count = 10)
        {
            if (page <= 0 || count <= 0)
            {
                return BadRequest("Page or count is less or equal to 0");
            }
            
            return Ok(_Repo.GetAll(count, page));
        }

        [HttpGet("Count")]
        public ActionResult<int> GetStudentsCount()
        {
            return Ok(_Repo.StudentsCount()
);
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = _Repo.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost(nameof(AddStudent))]
        public IActionResult AddStudent([FromBody] StudentDto student)
        {
            if (string.IsNullOrWhiteSpace(student.Email))
            {
                return BadRequest("Email is required");
            }

            if (string.IsNullOrWhiteSpace(student.UserName))
            {
                return BadRequest("Name is required");
            }


            _Repo.Add(new Student { Email = student.Email, UserName = student.UserName,DepartmentId=student.DepartmentId });
            context.SaveChanges();
            //
            return StatusCode((int)System.Net.HttpStatusCode.Created);
        }

        [HttpPut(nameof(EditStudent))]
        public IActionResult EditStudent([FromBody] StudentDto student)
        {
            if (string.IsNullOrWhiteSpace(student.Email))
            {
                return BadRequest("Email is required");
            }

            if (string.IsNullOrWhiteSpace(student.UserName))
            {
                return BadRequest("Name is required");
            }


            _Repo.Update(new Student { Email = student.Email, UserName = student.UserName, DepartmentId = student.DepartmentId ,Id=student.Id});
            context.SaveChanges();
            //
            return StatusCode((int)System.Net.HttpStatusCode.Created);
        }

        [HttpDelete("DeleteStudents/{id}")]
        public IActionResult DeleteStudents(int id)
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

    public record class StudentDto(int Id,string UserName, string Email,int DepartmentId);
}
