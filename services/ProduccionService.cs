using Proyecto_Pastel.DAOs;
using Proyecto_Pastel.Entities;

namespace Proyecto_Pastel.services
{
    public class ProduccionService : IProduccionService
    {
        private readonly proyecto_pastelContext db = new();
        private readonly InventarioDAO inventarioDAO = new();
        private readonly PostreDAO postreDAO = new();
        private readonly RecetaDAO recetaDAO = new();

        public string ProducirPostre(int idReceta, int idUsuario, int cantidad)
        {
            using var transaction = db.Database.BeginTransaction();

            try
            {
                if (cantidad <= 0)
                    return "Cantidad inválida para producir.";

                var receta = db.recetas.FirstOrDefault(r => r.id_receta == idReceta);
                if (receta == null)
                    return "Receta no encontrada.";

                var postre = postreDAO.ObtenerPorId(receta.id_postre);
                if (postre == null)
                    return "Postre vinculado a la receta no encontrado.";

                var ingredientesReceta = db.ingredientes_receta
                    .Where(ir => ir.id_receta == idReceta)
                    .ToList();

                // Validación de inventario suficiente para la cantidad deseada
                foreach (var ingrediente in ingredientesReceta)
                {
                    var disponible = inventarioDAO.ObtenerPorId(ingrediente.id_ingrediente);
                    if (disponible == null)
                        return $"Ingrediente con ID {ingrediente.id_ingrediente} no encontrado en inventario.";

                    var requerido = ingrediente.cantidad * cantidad;
                    if (disponible.cantidad < requerido)
                        return $"No hay suficiente {disponible.nombre} en inventario. Se requieren {requerido} {ingrediente.unidad}. Disponible: {disponible.cantidad} {disponible.unidad}.";
                }

                // Descontar ingredientes y registrar movimientos
                foreach (var ingrediente in ingredientesReceta)
                {
                    var existente = inventarioDAO.ObtenerPorId(ingrediente.id_ingrediente);
                    var cantidadUsada = ingrediente.cantidad * cantidad;

                    existente.cantidad -= cantidadUsada;
                    inventarioDAO.ActualizarIngrediente(existente);

                    db.movimientos_inventario.Add(new movimientos_inventario
                    {
                        id_ingrediente = ingrediente.id_ingrediente,
                        id_usuario = idUsuario,
                        tipo_movimiento = "salida",
                        motivo = "producción",
                        cantidad = cantidadUsada,
                        descripcion = $"Usado para producir {cantidad}x '{postre.nombre}'",
                        fecha_movimiento = DateTime.Now
                    });
                }

                // Aumentar inventario del postre producido
                postre.cantidad_disponible += cantidad;
                postreDAO.ActualizarPostre(postre);

                db.SaveChanges();
                transaction.Commit();

                return $"Se produjo correctamente {cantidad}x '{postre.nombre}'.";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error al procesar producción: {ex.Message}";
            }
        }
    }
}