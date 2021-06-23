using CadastroAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroAPI.Database
{
    public static class ClienteDAO
    {
        public static bool AdicionaCliente(ClienteAuxiliar cliente, ApiContext apiContext)
        {
            Cliente encontrarCliente = EncontrarCliente(cliente, apiContext);

            //Cliente cliente1 = apiContext.Clientes.Where(c => c.CPF == cliente.CPF || c.Id == cliente.Id).First();

            if (encontrarCliente !=null)//Validação para não adicionar cliente com o mesmo ID ou CPF
                return false;

            List<Cliente> listaClientes = apiContext.Clientes.ToList();

            int ultimoIdCliente = listaClientes.Count() > 0 ? listaClientes.OrderBy(c=> c.Id).Last().Id : 0; //pega o último ID de cliente
            cliente.Id = ultimoIdCliente + 1;
            apiContext.Clientes.Add(cliente);

            int ultimoEnderecoID = apiContext.Enderecos.ToList().Count() > 0 ? apiContext.Enderecos.ToList().OrderBy(e => e.IdCliente).Last().Id : 0; //pega o último ID de endereço

            foreach (Endereco endereco in cliente.Enderecos)
            {
                ultimoEnderecoID++;
                endereco.Id = ultimoEnderecoID;
                endereco.IdCliente = cliente.Id;
                apiContext.Enderecos.Add(endereco);
            }
            apiContext.SaveChanges();

            return true;
        }

        public static ClienteAuxiliar RetornaClientePorCPF(string cpf, ApiContext apiContext)
        {
            List<Cliente> clientes = apiContext.Clientes.ToList();
            int count = clientes.Where(c => c.CPF == cpf).Count();
            if(count == 0)//CPF não encontrado
                return null;

            Cliente cliente = clientes.Where(c => c.CPF == cpf).First();
            ClienteAuxiliar auxiliar = new ClienteAuxiliar
            {
                Id = cliente.Id,
                CPF = cliente.CPF,
                Email = cliente.Email,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                Enderecos = RetornaEnderecosCliente(cliente.Id, apiContext.Enderecos.ToList())
            };

            return auxiliar;
        }

        public static List<ClienteAuxiliar> RetornarClientes(ApiContext apiContext)
        {
            List<ClienteAuxiliar> clienteAuxiliars = new List<ClienteAuxiliar>();
            List<Endereco> enderecos = apiContext.Enderecos.ToList();
            List<Cliente> clientes = apiContext.Clientes.ToList();

            foreach (Cliente cliente in clientes)
            {
                ClienteAuxiliar auxiliar = new ClienteAuxiliar 
                {
                    Id = cliente.Id,
                    CPF = cliente.CPF,
                    Email = cliente.Email,
                    Nome = cliente.Nome,
                    Telefone = cliente.Telefone,
                    Enderecos = RetornaEnderecosCliente(cliente.Id,enderecos)
                };
                clienteAuxiliars.Add(auxiliar);
            }

            return clienteAuxiliars;
        }
        public static bool ExcluirClientePorId(int id,ApiContext apiContext)
        {
            List<Cliente> clientes = apiContext.Clientes.ToList();
            Cliente cliente = clientes.Find(p => p.Id == id);

            if (cliente == null)
                return false;

            List<Endereco> enderecos = RetornaEnderecosCliente(cliente.Id, apiContext.Enderecos.ToList());

            apiContext.Enderecos.RemoveRange(enderecos);
            apiContext.Clientes.Remove(cliente);

            apiContext.SaveChanges();

            return true;

        }
        public static bool EditarCliente(ClienteAuxiliar clienteAuxiliar, ApiContext apiContext)
        {
            Cliente cliente = EncontrarCliente(clienteAuxiliar, apiContext);
            
            if (cliente == null)
                return false;

            cliente.Email = clienteAuxiliar.Email;
            cliente.Nome = clienteAuxiliar.Nome;
            cliente.Telefone = clienteAuxiliar.Telefone;

            int ultimoEnderecoID = apiContext.Enderecos.ToList().OrderBy(e => e.IdCliente).Last().Id;//pega ultimo ID de endereço

            List<Endereco> enderecos = RetornaEnderecosCliente(cliente.Id, apiContext.Enderecos.ToList());
            apiContext.Enderecos.RemoveRange(enderecos);

            
            foreach (Endereco endereco in clienteAuxiliar.Enderecos)
            {
                ultimoEnderecoID++;
                endereco.Id = ultimoEnderecoID;
                endereco.IdCliente = cliente.Id;
            }

            apiContext.AddRange(clienteAuxiliar.Enderecos);

            apiContext.SaveChanges();

            return true;

        }
        private static List<Endereco> RetornaEnderecosCliente(int idCliente,List<Endereco> enderecos)
        {
           return enderecos.Where(endereco => endereco.IdCliente == idCliente).ToList();
        }
        private static Cliente EncontrarCliente(Cliente cliente,ApiContext apiContext)
        {
            return apiContext.Clientes.ToList().Find(Cliente => Cliente.Id == cliente.Id || Cliente.CPF == cliente.CPF);
        }
        private static int RetornaUltimoClienteId(ApiContext apiContext)
        {
            List<Cliente> clientes = apiContext.Clientes.ToList();
            return clientes.Count() == 0 ? 0 : clientes.Last().Id;
        }
        private static int RetornaUltimoEnderecoId(ApiContext apiContext)
        {
            List<Endereco> enderecos = apiContext.Enderecos.ToList();
            return enderecos.Count() == 0 ? 0 : enderecos.Last().Id;
        }
    }
}
