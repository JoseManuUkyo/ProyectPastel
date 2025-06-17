using System.Collections.Generic;
using System.Linq;
using Proyecto_Pastel.Entities;

namespace Proyecto_Pastel.DAOs
{
    public class InventarioDAO
    {
        private proyecto_pastelContext dbContext;

        public InventarioDAO()
        {
            dbContext = new proyecto_pastelContext();
        }

        public List<inventario> ConsultarInventario()
        {
            return dbContext.inventario.ToList();
        }

        public void InsertarIngrediente(inventario nuevo)
        {
            dbContext.inventario.Add(nuevo);
            dbContext.SaveChanges();
        }

        public inventario ObtenerPorId(int id)
        {
            return dbContext.inventario.FirstOrDefault(i => i.id_ingrediente == id);
        }

        public void ActualizarIngrediente(inventario ingrediente)
        {
            var existente = dbContext.inventario.Find(ingrediente.id_ingrediente);
            if (existente != null)
            {
                existente.nombre = ingrediente.nombre;
                existente.cantidad = ingrediente.cantidad;
                existente.unidad = ingrediente.unidad;
                dbContext.SaveChanges();
            }
        }

        public void EliminarIngrediente(int id)
        {
            var ingrediente = dbContext.inventario.Find(id);
            if (ingrediente != null)
            {
                dbContext.inventario.Remove(ingrediente);
                dbContext.SaveChanges();
            }
        }
    }
}