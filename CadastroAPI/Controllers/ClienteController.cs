using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CadastroAPI.Models;
using CadastroAPI.Database;

namespace CadastroAPI.Controllers
{
    [ApiController]
    [Route("api/Cliente")]
    public class ClienteController : Controller
    {
        private readonly ApiContext apiContext;

        public ClienteController(ApiContext apiContext)
        {
            this.apiContext = apiContext;
        }

        [HttpGet]
        [Route("RetornaClientes")]
        public List<ClienteAuxiliar> RetornaClientes()
        {
           return ClienteBusiness.RetornaClientes(apiContext); 
        }

        [HttpGet]
        [Route("RetornaCliente/{cpf?}")]
        public ClienteAuxiliar RetornaCliente(string cpf)
        {
            return ClienteBusiness.RetornaClientePorCPF(cpf,apiContext);
        }

        [HttpPost]
        [Route("CadastraCliente")]
        public bool CadastraCliente()
        {
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                string jsonBody = reader.ReadToEnd();
                ClienteAuxiliar clienteDesserializado =  JsonConvert.DeserializeObject<ClienteAuxiliar>(jsonBody);
                bool result = ClienteBusiness.CadastraCliente(clienteDesserializado, apiContext);
                return result;
            }
        }
    }
}
