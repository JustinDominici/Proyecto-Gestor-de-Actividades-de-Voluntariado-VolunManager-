namespace VolunManager.Application.Dtos.Roles
{
    public class RolDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public int CantidadVoluntarios { get; set; }
    }
}
