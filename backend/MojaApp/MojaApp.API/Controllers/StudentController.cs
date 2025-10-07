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
        [HttpGet]
        public Student GetById(int id)
        {
            Student? s = StudentStorage.Students.Where(x=>x.Id == id).FirstOrDefault();

            if (s == null)
                throw new Exception("nema studenta");

            return s;
        }
    }
}
