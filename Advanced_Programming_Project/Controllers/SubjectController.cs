using Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;

namespace Controllers
{
    [ApiController]
    [Route("/Subjects")]
    public class SubjectsController : ControllerBase
    {
        readonly SubjectRepository _Repo;
        readonly SubjectLectureRepository _SubLectRepo;

        readonly EFContext context;

        public SubjectsController(SubjectRepository Repo, SubjectLectureRepository SubLectRepo, EFContext context)
        {
            _Repo = Repo;
            _SubLectRepo= SubLectRepo;
            this.context = context;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Subject>> GetSubjects(int page = 1, int count = 10)
        {
            if (page <= 0 || count <= 0)
            {
                return BadRequest("Page or count is less or equal to 0");
            }

            return Ok(_Repo.GetAll(count, page));
        }


        [HttpGet("{id}")]
        public ActionResult<Subject> GetSubject(int id)
        {
            var Subject = _Repo.GetById(id);

            if (Subject == null)
            {
                return NotFound();
            }

            return Ok(Subject);
        }

        [HttpPost(nameof(AddSubject))]
        public IActionResult AddSubject([FromBody] SubjectDto Subject)
        {

            _Repo.Add(new Subject {Name=Subject.Name, DepartmentId = Subject.DepartmentId, MinimumDegree = Subject.MinimumDegree, Term = Subject.Term,Year=Subject.Year });
            context.SaveChanges();

            return StatusCode((int)System.Net.HttpStatusCode.Created);
        }

        [HttpPut(nameof(EditSubject))]
        public IActionResult EditSubject([FromBody] SubjectDto Subject)
        {



            _Repo.Update(new Subject {Id=Subject.Id, Name = Subject.Name, DepartmentId = Subject.DepartmentId, MinimumDegree = Subject.MinimumDegree, Term = Subject.Term, Year = Subject.Year });
            context.SaveChanges();
            //
            return StatusCode((int)System.Net.HttpStatusCode.Created);
        }

        [HttpDelete("DeleteSubjects/{id}")]
        public IActionResult DeleteSubjects(int id)
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

        [HttpGet("GetLectures/{id}")]
        public ActionResult<IEnumerable<SubjectLecture>> GetLectures(int id,int count=10,int PageIndex=1)
        {

            return Ok(_SubLectRepo.GetSubjectLecture(count,PageIndex,id));
        }
    }

    public record class SubjectDto(int Id, int DepartmentId, string Name, int MinimumDegree,short Term,short Year);
}
