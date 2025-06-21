using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Pastel.DAOs;
using Proyecto_Pastel.services;
using Proyecto_Pastel.Entities;
using Proyecto_Pastel.Models;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_Pastel.Pages.Production
{
    public class IndexModel : PageModel
    {
        private readonly IProduccionService _produccionService;
        private readonly RecetaDAO _recetaDAO;
        private readonly PostreDAO _postreDAO;

        public IndexModel(IProduccionService produccionService)
        {
            _produccionService = produccionService;
            _recetaDAO = new RecetaDAO();
            _postreDAO = new PostreDAO();
        }

        [BindProperty]
        public int? IdRecetaSeleccionada { get; set; }

        [BindProperty]
        public int CantidadProduccion { get; set; } = 1;

        public List<SelectListItem> RecetasSelectList { get; set; } = new();
        public string? Mensaje { get; set; }

        public List<IngredientePreview> IngredientesPrevios { get; set; } = new();

        public void OnGet()
        {
            var recetas = _recetaDAO.ObtenerTodasConPostres();

            RecetasSelectList = recetas
                .Select(r => new SelectListItem
                {
                    Value = r.IdReceta.ToString(),
                    Text = $"{r.NombrePostre} - {r.Descripcion}"
                })
                .ToList();
        }

        public IActionResult OnPost(string accion)
        {
            OnGet(); // Recargar la lista

            if (IdRecetaSeleccionada == null || CantidadProduccion <= 0)
            {
                Mensaje = "Selecciona una receta y una cantidad válida.";
                return Page();
            }

            var receta = _recetaDAO.ObtenerPorIdConIngredientes(IdRecetaSeleccionada.Value);

            if (receta == null)
            {
                Mensaje = "Receta no encontrada.";
                return Page();
            }

            if (accion == "previsualizar")
            {
                IngredientesPrevios = receta.Ingredientes.Select(i =>
                {
                    decimal cantidadUsar = i.CantidadNecesaria * CantidadProduccion;
                    return new IngredientePreview
                    {
                        NombreIngrediente = i.Nombre,
                        Disponible = i.IngredienteRef.cantidad,
                        AUsar = cantidadUsar,
                        Restante = i.IngredienteRef.cantidad - cantidadUsar
                    };
                }).ToList();

                return Page();
            }

            if (accion == "producir")
            {
                try
                {
                    int idUsuario = 1; // Temporal
                    Mensaje = _produccionService.ProducirPostre(IdRecetaSeleccionada.Value, idUsuario, CantidadProduccion);
                }
                catch (Exception ex)
                {
                    Mensaje = $"Error al producir: {ex.Message}";
                }
            }

            return Page();
        }

        public class IngredientePreview
        {
            public string NombreIngrediente { get; set; } = "";
            public decimal Disponible { get; set; }
            public decimal AUsar { get; set; }
            public decimal Restante { get; set; }
        }
    }
}