using Microsoft.AspNetCore.Mvc.RazorPages;
using Proyecto_Pastel.Entities;
using Proyecto_Pastel.DAOs;

namespace Proyecto_Pastel.Pages.Sales;

public class SalesModel : PageModel
{
    public List<ventas> Ventas { get; set; } = new();

    public void OnGet()
    {
        Ventas = new VentaDAO().ObtenerVentasConDetalles();
    }
}