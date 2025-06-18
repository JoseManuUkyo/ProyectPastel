using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Pastel.Entities;

public partial class detalle_venta
{
    public int id_detalle { get; set; }

    public int id_venta { get; set; }

    public int id_postre { get; set; }

    public int cantidad { get; set; }

    public decimal precio_unitario { get; set; }
    
    public virtual postres id_postreNavigation { get; set; } = null!;

    public virtual ventas id_ventaNavigation { get; set; } = null!;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public decimal? subtotal { get; set; }
}
