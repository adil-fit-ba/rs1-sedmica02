using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MojaApp.API.Data;
using MojaApp.API.Models;

namespace MojaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet("{id}")]
        public Student GetById(int id)
        {
            Student? s = StudentStorage.Students.Where(x=>x.Id == id).FirstOrDefault();

            if (s == null)
                throw new Exception("nema studenta");

            return s;
        }

        [HttpPost]
        public Student Dodaj(string ime, string prezime)
        {
            var maxID = StudentStorage.Students.Max(x => x.Id);

            var s = new Student
            {
                Id = maxID + 1,
                Ime = ime,
                Prezime = prezime
            };
            StudentStorage.Students.Add(s);
            return s;
        }

        [HttpGet]
        public List<Student> GetAll()
        {
            return StudentStorage.Students;
        }
    }
}
