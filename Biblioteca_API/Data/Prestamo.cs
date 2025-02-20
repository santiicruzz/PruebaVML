using System;
using System.Collections.Generic;

namespace Biblioteca_API.Data;

public partial class Prestamo
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int Idlibro { get; set; }

    public DateTime? FechaPrestamo { get; set; }

    public DateTime? FechaDevolucion { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual Libro IdlibroNavigation { get; set; } = null!;
}
