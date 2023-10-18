using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscuelaCRUD.Shared
{
    public class MateriaDTO
    {
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public int IdMateria { get; set; }

        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string? NomnbreMateria { get; set; }

    }
}
