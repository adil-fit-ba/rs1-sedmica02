namespace MojaApp.API.Controllers.Dtos
{
    public record StudentGetAllResponse(
        int StudentId,
        string Ime,
        string Prezime,
        StudentGetAllResponseOpstina? Opstina
    );

    public record StudentGetAllResponseOpstina
    (
        string Naziv,
        string Ptt
    );
}
