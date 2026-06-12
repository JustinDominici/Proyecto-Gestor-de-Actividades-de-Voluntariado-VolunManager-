namespace VolunManager.Api.DTOs.Jornadas
{
    public class JornadaDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }

        public string Lugar { get; set; } = string.Empty;

        public int HorasEstimadas { get; set; }

        public int VoluntarioId { get; set; }

        public string? NombreVoluntario { get; set; }
    }
}