using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EscuelaCRUD.Shared
{
    public class EstudianteDTO
    {
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public int IdEstudiante { get; set; }

        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string? NombreEstudiante { get; set; }

    }
}
