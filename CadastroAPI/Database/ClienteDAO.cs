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
            Cliente cliente1 = apiContext.Clientes.Where(c => c.CPF == cliente.CPF || c.Id == cliente.Id).First();

            if (cliente1 != null)//Validação para não adicionar cliente com o mesmo ID ou CPF
                return false;

            int ultimoIdCliente = apiContext.Clientes.ToList().OrderBy(c=> c.Id).Last().Id; //pega o último ID de cliente
            cliente.Id = ultimoIdCliente + 1;
            apiContext.Clientes.Add(cliente);

            int ultimoEnderecoID = apiContext.Enderecos.ToList().OrderBy(e => e.IdCliente).Last().Id; //pega o último ID de endereço

            foreach (Endereco endereco in cliente.Enderecos)
            {
                ultimoEnderecoID++;
                endereco.Id = ultimoEnderecoID;
                apiContext.Enderecos.Add(endereco);
            }
            apiContext.SaveChanges();

            return true;
        }

        public static ClienteAuxiliar RetornaClientePorCPF(string cpf, ApiContext apiContext)
        {
            int count = apiContext.Clientes.ToList().Where(c => c.CPF == cpf).Count();
            if(count == 0)//CPF não encontrado
                return null;

            Cliente cliente = apiContext.Clientes.ToList().Where(c => c.CPF == cpf).First();
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
        public static bool ExcluirClientePorCPF(string cpf,ApiContext apiContext)
        {
            Cliente cliente = apiContext.Clientes.Where(c => c.CPF == cpf).First();

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
            Cliente cliente = apiContext.Clientes.Where(c => c.CPF == clienteAuxiliar.CPF).First();
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
            }

            apiContext.AddRange(clienteAuxiliar.Enderecos);

            apiContext.SaveChanges();

            return true;

        }
        private static List<Endereco> RetornaEnderecosCliente(int idCliente,List<Endereco> enderecos)
        {
           return enderecos.Where(endereco => endereco.IdCliente == idCliente).ToList();
        }
    }
}
