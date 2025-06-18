using Proyecto_Pastel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Pastel.DAOs
{
    public class VentaDAO
    {
        private proyecto_pastelContext db = new();

        public void RegistrarVenta(ventas venta, List<detalle_venta> detalles)
        {
            db.ventas.Add(venta);
            db.SaveChanges();

            foreach (var d in detalles)
            {
                d.id_venta = venta.id_venta;
                db.detalle_venta.Add(d);
            }

            db.SaveChanges();
        }

        public List<ventas> ObtenerVentasConDetalles()
        {
            return db.ventas
                .Include(v => v.id_usuarioNavigation)
                .Include(v => v.detalle_venta)
                    .ThenInclude(d => d.id_postreNavigation)
                .OrderByDescending(v => v.fecha_venta)
                .ToList();
        }

        public void InsertarVentaConDetalle(ventas nuevaVenta, List<detalle_venta> detalles)
        {
            db.ventas.Add(nuevaVenta);
            db.SaveChanges(); // Guarda para obtener el id_venta generado

            foreach (var detalle in detalles)
            {
                detalle.id_venta = nuevaVenta.id_venta;
                db.detalle_venta.Add(detalle);
            }

            db.SaveChanges();
        }
    }
}