using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Contraseña { get; set; }

    public virtual ICollection<Historial> Historials { get; } = new List<Historial>();
}
