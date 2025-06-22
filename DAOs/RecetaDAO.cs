using Proyecto_Pastel.Entities;
using Proyecto_Pastel.Models;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_Pastel.DAOs
{
    public class RecetaDAO
    {
        private proyecto_pastelContext db = new();

        // Para llenar el select de recetas en la vista
        public List<RecetaConPostreDTO> ObtenerTodasConPostres()
        {
            return db.recetas
                .Join(db.postres,
                      receta => receta.id_postre,
                      postre => postre.id_postre,
                      (receta, postre) => new RecetaConPostreDTO
                      {
                          IdReceta = receta.id_receta,
                          IdPostre = postre.id_postre,
                          Descripcion = receta.descripcion,
                          NombrePostre = postre.nombre
                      })
                .ToList();
        }

        // Por si se necesita solo la receta cruda
        public recetas? ObtenerPorId(int id)
        {
            return db.recetas.FirstOrDefault(r => r.id_receta == id);
        }

        // El método que necesita la producción para mostrar ingredientes y cantidades
        public RecetaConIngredientesDTO? ObtenerPorIdConIngredientes(int id)
        {
            var receta = db.recetas.FirstOrDefault(r => r.id_receta == id);
            if (receta == null) return null;

            var postre = db.postres.FirstOrDefault(p => p.id_postre == receta.id_postre);

            var ingredientes = db.ingredientes_receta
                .Where(ir => ir.id_receta == id)
                .Join(db.inventario,
                      ir => ir.id_ingrediente,
                      ing => ing.id_ingrediente,
                      (ir, ing) => new IngredienteRecetaDTO
                      {
                          IdIngrediente = ing.id_ingrediente,
                          Nombre = ing.nombre,
                          CantidadNecesaria = ir.cantidad,
                          IngredienteRef = ing
                      })
                .ToList();

            return new RecetaConIngredientesDTO
            {
                IdReceta = receta.id_receta,
                Descripcion = receta.descripcion,
                NombrePostre = postre?.nombre ?? "Sin nombre",
                Ingredientes = ingredientes
            };
        }
    }
}