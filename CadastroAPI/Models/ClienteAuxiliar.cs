using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroAPI.Models
{
    public class ClienteAuxiliar : Cliente
    {
        public List<Endereco> Enderecos { get; set; }

    }
}
