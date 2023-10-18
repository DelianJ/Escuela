using System;
using System.Collections.Generic;

namespace Escuela_API.Models;

public partial class MateriasProfesore
{
    public int IdProfesor { get; set; }

    public int IdMateria { get; set; }

    public virtual Materia IdMateriaNavigation { get; set; } = null!;

    public virtual Profesore IdProfesorNavigation { get; set; } = null!;
}
