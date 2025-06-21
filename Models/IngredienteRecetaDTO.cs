using Proyecto_Pastel.Entities;

namespace Proyecto_Pastel.Models
{
    public class IngredienteRecetaDTO
    {
        public int IdIngrediente { get; set; }
        public string Nombre { get; set; } = "";
        public decimal CantidadNecesaria { get; set; }

        // Aquí usamos la entidad real: inventario
        public inventario IngredienteRef { get; set; } = new();
    }
}