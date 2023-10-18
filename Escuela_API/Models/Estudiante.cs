using System;
using System.Collections.Generic;

namespace Escuela_API.Models;

public partial class Estudiante
{
    public int IdEstudiante { get; set; }

    public string? NombreEstudiante { get; set; }

    public virtual ICollection<MateriasEstudiante> MateriasEstudiantes { get; set; } = new List<MateriasEstudiante>();
}
