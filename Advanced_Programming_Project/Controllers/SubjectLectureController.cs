using Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;

namespace Controllers
{
    [ApiController]
    [Route("/SubjectLectures")]
    public class SubjectLecturesController : ControllerBase
    {
        readonly SubjectLectureRepository _Repo;
        readonly EFContext context;

        public SubjectLecturesController(SubjectLectureRepository Repo, EFContext context)
        {
            _Repo = Repo;
            this.context = context;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<SubjectLecture>> GetSubjectLectures(int page = 1, int count = 10)
        {
            if (page <= 0 || count <= 0)
            {
                return BadRequest("Page or count is less or equal to 0");
            }

            return Ok(_Repo.GetAll(count, page));
        }


        [HttpGet("{id}")]
        public ActionResult<SubjectLecture> GetSubjectLecture(int id)
        {
            var SubjectLecture = _Repo.GetById(id);

            if (SubjectLecture == null)
            {
                return NotFound();
            }

            return Ok(SubjectLecture);
        }

        [HttpPost(nameof(AddSubjectLecture))]
        public IActionResult AddSubjectLecture([FromBody] SubjectLectureDto SubjectLecture)
        {

            _Repo.Add(new SubjectLecture {SubjectId=SubjectLecture.SubjectId, Title = SubjectLecture.Title, Content = SubjectLecture.Content });
            context.SaveChanges();

            return StatusCode((int)System.Net.HttpStatusCode.Created);
        }

        [HttpPut(nameof(EditSubjectLecture))]
        public IActionResult EditSubjectLecture([FromBody] SubjectLectureDto SubjectLecture)
        {



            _Repo.Update(new SubjectLecture { Id = SubjectLecture.Id, Title = SubjectLecture.Title, SubjectId = SubjectLecture.SubjectId,Content=SubjectLecture.Content });
            context.SaveChanges();
            //
            return StatusCode((int)System.Net.HttpStatusCode.Created);
        }

        [HttpDelete("DeleteSubjectLectures/{id}")]
        public IActionResult DeleteSubjectLectures(int id)
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

    public record class SubjectLectureDto(int Id, int SubjectId, string Title, string Content);
}
