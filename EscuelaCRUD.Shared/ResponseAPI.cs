using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscuelaCRUD.Shared
{
    public class ResponseAPI<T>
    {

        public Boolean esValido {  get; set; }
        public T? valor {  get; set; }
        public String? mensaje {  get; set; }

    }
}
