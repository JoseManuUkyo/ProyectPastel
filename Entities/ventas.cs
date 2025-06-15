using System;
using System.Collections.Generic;

namespace Proyecto_Pastel.Entities;

public partial class ventas
{
    public int id_venta { get; set; }

    public int? id_usuario { get; set; }

    public DateTime? fecha_venta { get; set; }

    public virtual ICollection<detalle_venta> detalle_venta { get; set; } = new List<detalle_venta>();

    public virtual usuarios? id_usuarioNavigation { get; set; }
}
