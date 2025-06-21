using System;
using System.Collections.Generic;

namespace Proyecto_Pastel.Entities;

public partial class inventario
{
    public int id_ingrediente { get; set; }

    public string nombre { get; set; } = null!;

    public decimal cantidad { get; set; }

    public string unidad { get; set; } = null!;

    public DateTime? fecha_actualizacion { get; set; }

    public virtual ICollection<ingredientes_receta> ingredientes_receta { get; set; } = new List<ingredientes_receta>();

    public virtual ICollection<movimientos_inventario> movimientos_inventario { get; set; } = new List<movimientos_inventario>();
}
