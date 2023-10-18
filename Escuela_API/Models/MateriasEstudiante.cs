using System;
using System.Collections.Generic;

namespace Escuela_API.Models;

public partial class MateriasEstudiante
{
    public int IdMateriasEstudiantes { get; set; }

    public int IdEstudiante { get; set; }

    public int IdMateria { get; set; }

    public decimal? Calificacion { get; set; }

    public virtual Estudiante IdEstudianteNavigation { get; set; } = null!;

    public virtual Materia IdMateriaNavigation { get; set; } = null!;
}
