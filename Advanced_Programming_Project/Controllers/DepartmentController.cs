using Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;

namespace Controllers
{
    [ApiController]
    [Route("/Departments")]
    public class DepartmentsController : ControllerBase
    {
        readonly DepartmentRepository _Repo;
        readonly StudentRepository _StudentRepo;

        readonly EFContext context;

        public DepartmentsController(DepartmentRepository Repo, StudentRepository StudentRepo, EFContext context)
        {
            _Repo = Repo;
            _StudentRepo= StudentRepo;
            this.context = context;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Department>> GetDepartments(int page = 1, int count = 10)
        {
            if (page <= 0 || count <= 0)
            {
                return BadRequest("Page or count is less or equal to 0");
            }

            return Ok(_Repo.GetAll(count, page));
        }


        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartment(int id)
        {
            var Department = _Repo.GetById(id);

            if (Department == null)
            {
                return NotFound();
            }

            return Ok(Department);
        }

        [HttpPost(nameof(AddDepartment))]
        public IActionResult AddDepartment([FromBody] DepartmentDto Department)
        {

            if (string.IsNullOrWhiteSpace(Department.Name))
            {
                return BadRequest("Name is required");
            }


            _Repo.Add(new Department { Name = Department.Name });
            context.SaveChanges();
            //
            return StatusCode((int)System.Net.HttpStatusCode.Created);
        }

        [HttpPut(nameof(EditDepartment))]
        public IActionResult EditDepartment([FromBody] DepartmentDto Department)
        {
            if (string.IsNullOrWhiteSpace(Department.Name))
            {
                return BadRequest("Name is required");
            }


            _Repo.Update(new Department { Id = Department.Id, Name = Department.Name });
            context.SaveChanges();
            //
            return StatusCode((int)System.Net.HttpStatusCode.Created);
        }

        [HttpDelete("DeleteDepartments/{id}")]
        public IActionResult DeleteDepartments(int id)
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

        [HttpGet("Students/{id}")]
        public ActionResult<IEnumerable<Student>> GetStudents(int id,int page = 1, int count = 10)
        {
            if (page <= 0 || count <= 0)
            {
                return BadRequest("Page or count is less or equal to 0");
            }

            return Ok(_StudentRepo.GetStudentsByDepartmentId(count, page,id));
        }
    }

    public record class DepartmentDto(int Id, string Name);
}
