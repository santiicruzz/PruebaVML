using System;
using System.Collections.Generic;

namespace Biblioteca_API.Data;

public partial class Usuario
{
    public int Id { get; set; }

    public int? Cedula { get; set; }

    public string? PrimerNombre { get; set; }

    public string? SegundoNombre { get; set; }

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }

    public string? CorreElectronico { get; set; }

    public string? NumeroContacto { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
