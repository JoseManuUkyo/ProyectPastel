using System;
using System.Collections.Generic;

namespace Proyecto_Pastel.Entities;

public partial class ingredientes_receta
{
    public int id_ingrediente_receta { get; set; }

    public int id_receta { get; set; }

    public int id_ingrediente { get; set; }

    public decimal cantidad { get; set; }

    public string unidad { get; set; } = null!;

    public virtual inventario id_ingredienteNavigation { get; set; } = null!;

    public virtual recetas id_recetaNavigation { get; set; } = null!;
}
