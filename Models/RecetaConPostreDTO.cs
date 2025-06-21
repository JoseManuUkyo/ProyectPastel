namespace Proyecto_Pastel.Models
{
    public class RecetaConPostreDTO
    {
        public int IdReceta { get; set; }
        public string? Descripcion { get; set; }
        public int IdPostre { get; set; }
        public string NombrePostre { get; set; } = "";
    }
}