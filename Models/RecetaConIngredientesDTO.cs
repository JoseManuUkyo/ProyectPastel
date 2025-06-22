namespace Proyecto_Pastel.Models
{
    public class RecetaConIngredientesDTO
    {
        public int IdReceta { get; set; }
        public string NombrePostre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public List<IngredienteRecetaDTO> Ingredientes { get; set; } = new();
    }
}