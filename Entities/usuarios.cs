using System;
using System.Collections.Generic;

namespace Proyecto_Pastel.Entities;

public partial class usuarios
{
    public int id_usuario { get; set; }

    public string nombre { get; set; } = null!;

    public string correo { get; set; } = null!;

    public string contraseña { get; set; } = null!;

    public DateTime? fecha_registro { get; set; }

    public virtual ICollection<movimientos_inventario> movimientos_inventario { get; set; } = new List<movimientos_inventario>();

    public virtual ICollection<ventas> ventas { get; set; } = new List<ventas>();
}
