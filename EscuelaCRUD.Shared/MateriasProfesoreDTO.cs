using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EscuelaCRUD.Shared;

namespace EscuelaCRUD.Shared
{
    public class MateriasProfesoreDTO
    {
        public int IdProfesor { get; set; }

        public int IdMateria { get; set; }

        public MateriaDTO? Materia { get; set; }

        public ProfesoreDTO? Profesore { get; set; }

    }
}
