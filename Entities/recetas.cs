using System;
using System.Collections.Generic;

namespace Proyecto_Pastel.Entities;

public partial class recetas
{
    public int id_receta { get; set; }

    public int id_postre { get; set; }

    public string? descripcion { get; set; }

    public virtual postres id_postreNavigation { get; set; } = null!;

    public virtual ICollection<ingredientes_receta> ingredientes_receta { get; set; } = new List<ingredientes_receta>();
}
