using System;
using System.Collections.Generic;

namespace Proyecto_Pastel.Entities;

public partial class movimientos_inventario
{
    public int id_movimiento { get; set; }

    public int id_ingrediente { get; set; }

    public int id_usuario { get; set; }

    public string tipo_movimiento { get; set; } = null!;

    public string motivo { get; set; } = null!;

    public decimal cantidad { get; set; }

    public DateTime? fecha_movimiento { get; set; }

    public string? descripcion { get; set; }

    public virtual inventario id_ingredienteNavigation { get; set; } = null!;

    public virtual usuarios id_usuarioNavigation { get; set; } = null!;
}
