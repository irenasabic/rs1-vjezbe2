using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MojaApp.API.Controllers.Dtos;
using MojaApp.API.Data;
using MojaApp.API.Models;

namespace MojaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet("{id}")]
        public StudentGetByIdResponse GetById(int id)
        {
            StudentGetByIdResponse? s = StudentStorage.Studenti.Where(x => x.Id == id)
                .Select(x => new StudentGetByIdResponse
                (
                    x.Id,
                    x.Ime,
                    x.Prezime,
                    x.SlikaStudenta,
                    x.OpstinaRodjenjaId == null ? null : new StudentGetByIdResponseOpstina(x.OpstinaRodjenja.Description, "123")
                )).FirstOrDefault();

            if (s == null)
            {
                throw new Exception("Nema studenta");
            }
            else
                return s;
        }

        [HttpGet]
        public List<StudentGetAllResponse> GetAllStudents()
        {
            return StudentStorage.Studenti.Select(x => new StudentGetAllResponse
                (
                    x.Id,
                    x.Ime,
                    x.Prezime,
                    x.OpstinaRodjenja == null ? null : new StudentGetAllResponseOpstina(x.OpstinaRodjenja.Description, "123")
                )
            ).ToList();
        }

        [HttpPost]
        public int Dodaj([FromBody]StudentDodajRequest request)
        {
            var maxID = StudentStorage.Studenti.Max(x => x.Id);

            var s = new Student
            {
                Id = maxID + 1,
                Ime = request.Ime,
                Prezime = request.Prezime,
                 OpstinaRodjenjaId = request.OpstinaRodjenjaId,
                DatumRodjenja = request.DatumRodjenja
            };
            StudentStorage.Studenti.Add(s);
            return s.Id;
        }

        [HttpDelete]
        public IActionResult Obrisi(int studentId)
        {
            var s = StudentStorage.Studenti.FirstOrDefault(x => x.Id == studentId);
            if (s is null)
                return BadRequest();
            StudentStorage.Studenti.Remove(s);
            return Ok();
        }
    }
}
