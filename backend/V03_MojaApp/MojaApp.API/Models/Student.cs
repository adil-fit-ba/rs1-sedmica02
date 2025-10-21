using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MojaApp.API.Models
{
    public class Student
    {
        public int Id { get; set; }
        public required string Ime { get; set; }
        public required string Prezime { get; set; }
        public required string BrojIndeksa { get; set; }

        public required int? OpstinaRodjenjaId { get; set; }
        public Opstina OpstinaRodjenja { get; set; }
        public required DateTime? DatumRodjenja { get; set; }
        public required DateTime CreatedTime { get; set; }
        public required string SlikaStudenta { get; set; }
    }
}
