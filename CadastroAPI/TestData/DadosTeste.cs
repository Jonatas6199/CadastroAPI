using CadastroAPI.Database;
using CadastroAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroAPI.TestData
{
    public static class DadosTeste
    {
        public static void AdicionarDadosTeste(ApiContext context)
        {
            AdicionarClientes(context);
            AdicionarEnderecos(context);

            context.SaveChanges();
        }

        #region Clientes
        private static void AdicionarClientes(ApiContext context)
        {
            Cliente cliente1 = new Cliente()
            {
                Id = 1,
                CPF = "27694151072",
                Email = "jason4315@moxkid.com",
                Nome = "Jason",
                Telefone = "997647814"
            };
            Cliente cliente2 = new Cliente()
            {
                Id = 2,
                CPF = "44557064027",
                Email = "patrick_stuart@protonmail.com",
                Nome = "Patrick",
                Telefone = "965969738"

            };
            Cliente cliente3 = new Cliente()
            {
                Id = 3,
                CPF = "42626755052",
                Email = "robert_england@mail.com",
                Nome = "Robert",
                Telefone = "44550622"
            };

            context.Clientes.Add(cliente1);
            context.Clientes.Add(cliente2);
            context.Clientes.Add(cliente3);
        }
        #endregion

        #region Endereços
        private static void AdicionarEnderecos(ApiContext context)
        {
            Endereco endereco1 = new Endereco()
            {
                Id = 1, IdCliente = 1, Bairro = "Vila Nova", Cidade = "Lagarto", Complemento = "Sobreloja 3",
                Estado = "Sergipe", Logradouro = "São Sebastião", Numero = 5117, Tipo = "Comercial"
            };
            
            Endereco endereco2 = new Endereco()
            {
                Id = 2, IdCliente = 1, Bairro = "Santo Antônio", Cidade = "Boa Vista", Complemento = "Terreo 9",
                Estado = "Roraima", Logradouro = "São Jorge", Numero = 8019, Tipo = "Residencial"
            };

            Endereco endereco3 = new Endereco()
            {
                Id = 3, IdCliente = 1, Bairro = "Bela Vista", Cidade = "Maceió", Complemento = "Terreo 6",
                Estado = "Alagoas", Logradouro = "Vinte", Numero = 59, Tipo = "Cobrança"
            };

            Endereco endereco4 = new Endereco()
            {
                Id = 4, IdCliente = 2, Bairro = "Santo Antônio", Cidade = "Santana", Complemento = "Lote 1",
                Estado = "Amapá", Logradouro = "Quinze de Novembro", Numero = 9875, Tipo = "Cobrança"
            };

            Endereco endereco5 = new Endereco()
            {
                Id = 5, IdCliente = 2, Bairro = "São Francisco", Cidade = "Governador Valadares", Complemento = "Cobertura 2",
                Estado = "Minas Gerais", Logradouro = "Quatorze", Numero = 9687, Tipo = "Residencial"
            };

            Endereco endereco6 = new Endereco()
            {
                Id = 6, IdCliente = 3, Bairro = "São José", Cidade = "Palmas", Complemento = "Fazenda 3",
                Estado = "Tocantins", Logradouro = "Dois", Numero = 4256, Tipo = "Entrega"
            };

            Endereco endereco7 = new Endereco()
            {
                Id = 7, IdCliente = 3, Bairro = "Industrial", Cidade = "Vespasiano", Complemento = "Fundos 3",
                Estado = "Minas Gerais", Logradouro = "Vinte e Quatro", Numero = 5719, Tipo = "Geral"
            };

            Endereco endereco8 = new Endereco()
            {
                Id = 8, IdCliente = 3, Bairro = "São Cristóvão", Cidade = "Muriaé", Complemento = "Bloco 6 Apartamento 23",
                Estado = "Minas Gerais",Logradouro = "Pernambuco",Numero = 1051, Tipo = "Residencial"
            };
            

            context.Enderecos.Add(endereco1);
            context.Enderecos.Add(endereco2);
            context.Enderecos.Add(endereco3);
            context.Enderecos.Add(endereco4);
            context.Enderecos.Add(endereco5);
            context.Enderecos.Add(endereco6);
            context.Enderecos.Add(endereco7);
            context.Enderecos.Add(endereco8);
        }
        #endregion

    }
}
