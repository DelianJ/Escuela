using System;
using System.Collections.Generic;

namespace Escuela_API.Models;

public partial class Materia
{
    public int IdMateria { get; set; }

    public string? NomnbreMateria { get; set; }

    public virtual ICollection<MateriasEstudiante> MateriasEstudiantes { get; set; } = new List<MateriasEstudiante>();
}
