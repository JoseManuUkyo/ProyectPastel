using System.Collections.Generic;
using Proyecto_Pastel.Entities;
using Proyecto_Pastel.DAOs;

namespace Proyecto_Pastel.services
{
    public class InventarioService
    {
        private InventarioDAO _inventarioDAO;

        public InventarioService()
        {
            _inventarioDAO = new InventarioDAO();
        }

        public List<inventario> ConsultarInventario()
        {
            return _inventarioDAO.ConsultarInventario();
        }

        public void AgregarIngrediente(inventario nuevo)
        {
            _inventarioDAO.InsertarIngrediente(nuevo);
        }

        public inventario ObtenerPorId(int id)
        {
            return _inventarioDAO.ObtenerPorId(id);
        }

        public void ActualizarIngrediente(inventario ingrediente)
        {
            _inventarioDAO.ActualizarIngrediente(ingrediente);
        }

        public void EliminarIngrediente(int id)
        {
            _inventarioDAO.EliminarIngrediente(id);
        }
    }
}