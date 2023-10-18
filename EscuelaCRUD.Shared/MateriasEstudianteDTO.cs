using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EscuelaCRUD.Shared;

namespace EscuelaCRUD.Shared
{
    public class MateriasEstudianteDTO
    {

        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public int IdEstudiante { get; set; }


        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public int IdMateria { get; set; }

        [Required]
        [Range(1,10, ErrorMessage = "El Campo {0} es requerido")]
        public decimal? Calificacion { get; set; }

        public EstudianteDTO? Estudiante { get; set; }

        public MateriaDTO? Materia { get; set; }

    }
}
