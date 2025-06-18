using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto_Pastel.Entities;
using Proyecto_Pastel.DAOs;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_Pastel.Pages.Sales
{
    public class IndexModel : PageModel
    {
        private readonly UsuarioDAO _usuarioDAO;
        private readonly PostreDAO _postreDAO;
        private readonly VentaDAO _ventaDAO;

        public IndexModel()
        {
            _usuarioDAO = new UsuarioDAO();
            _postreDAO = new PostreDAO();
            _ventaDAO = new VentaDAO();
        }

        [BindProperty]
        public string BusquedaUsuario { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<usuarios> UsuariosEncontrados { get; set; } = new();

        [BindProperty]
        public int? UsuarioSeleccionadoId { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<postres> ListaPostres { get; set; } = new();

        [TempData]
        public string DetallesVentaJson { get; set; }

        public List<DetalleVentaItem> DetallesVenta
        {
            get => string.IsNullOrEmpty(DetallesVentaJson)
                ? new List<DetalleVentaItem>()
                : System.Text.Json.JsonSerializer.Deserialize<List<DetalleVentaItem>>(DetallesVentaJson);
            set => DetallesVentaJson = System.Text.Json.JsonSerializer.Serialize(value);
        }

        public List<usuarios> UsuariosFiltrados => UsuariosEncontrados;
        public postres PostreSeleccionado => ListaPostres.FirstOrDefault(p => p.id_postre == UsuarioSeleccionadoId);
        public List<postres> PostresDisponibles => ListaPostres;
        public ventas Venta => new ventas { id_usuario = UsuarioSeleccionadoId };

        public void OnGet()
        {
            ListaPostres = _postreDAO.ObtenerTodos();
        }

        public IActionResult OnPostBuscarUsuario()
        {
            if (!string.IsNullOrWhiteSpace(BusquedaUsuario))
            {
                UsuariosEncontrados = _usuarioDAO.BuscarPorNombre(BusquedaUsuario);
            }

            ListaPostres = _postreDAO.ObtenerTodos();
            return Page();
        }

        public IActionResult OnPostAgregarPostre(int idPostre, int cantidad)
        {
            var postre = _postreDAO.ObtenerPorId(idPostre);
            if (postre != null && cantidad > 0)
            {
                var listaActual = DetallesVenta;
                listaActual.Add(new DetalleVentaItem
                {
                    IdPostre = postre.id_postre,
                    Nombre = postre.nombre,
                    Precio = postre.precio_base ?? 0,
                    Cantidad = cantidad
                });
                DetallesVenta = listaActual;
            }

            ListaPostres = _postreDAO.ObtenerTodos();
            return Page();
        }

        public IActionResult OnPostFinalizarVenta()
        {
            if (DetallesVenta == null || !DetallesVenta.Any())
            {
                ModelState.AddModelError(string.Empty, "No se han agregado productos a la venta.");
                ListaPostres = _postreDAO.ObtenerTodos();
                return Page();
            }

            var venta = new ventas
            {
                id_usuario = UsuarioSeleccionadoId
            };

            var detalles = DetallesVenta.Select(d => new detalle_venta
            {
                id_postre = d.IdPostre,
                cantidad = d.Cantidad,
                precio_unitario = d.Precio
            }).ToList();

            _ventaDAO.InsertarVentaConDetalle(venta, detalles);

            DetallesVenta = new List<DetalleVentaItem>();

            return RedirectToPage("Sales");
        }

        public class DetalleVentaItem
        {
            public int IdPostre { get; set; }
            public string Nombre { get; set; } = "";
            public decimal Precio { get; set; }
            public int Cantidad { get; set; }
            public decimal Subtotal => Precio * Cantidad;

            public string PostreNombre => Nombre;
            public int cantidad => Cantidad;
            public decimal precio_unitario => Precio;
        }
    }
}