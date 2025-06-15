using System;
using System.Collections.Generic;

namespace Proyecto_Pastel.Entities;

public partial class postres
{
    public int id_postre { get; set; }

    public string nombre { get; set; } = null!;

    public string? descripcion { get; set; }

    public decimal? precio_base { get; set; }

    public DateTime? fecha_creacion { get; set; }

    public virtual ICollection<detalle_venta> detalle_venta { get; set; } = new List<detalle_venta>();

    public virtual ICollection<recetas> recetas { get; set; } = new List<recetas>();
}
