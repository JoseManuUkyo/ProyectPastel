using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Proyecto_Pastel.Entities;
using Proyecto_Pastel.services;

namespace Proyecto_Pastel.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly InventarioService _inventarioService;

        public List<inventario> inventario { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            _inventarioService = new InventarioService();
        }

        public void OnGet()
        {
            try
            {
                inventario = _inventarioService.ConsultarInventario();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultar el inventario.");
                inventario = new List<inventario>();
            }
        }
    }
}