using System;
using System.Collections.Generic;

namespace Biblioteca_API.Data;

public partial class Libro
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Autor { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public int AnoPublicacion { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
