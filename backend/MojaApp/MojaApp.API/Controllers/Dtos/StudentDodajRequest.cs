namespace MojaApp.API.Controllers.Dtos
{
    public class StudentDodajRequest
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int OpstinaRodjenjaId { get; set; }
        public DateTime DatumRodjenja { get; set; }
    }
}
