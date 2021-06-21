using CadastroAPI.Database;
using CadastroAPI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CadastroAPI
{
    public static class ClienteBusiness
    {
        public static bool CadastraCliente(ClienteAuxiliar cliente, ApiContext apiContext)
        {
            if (Validacoes(cliente))
            {
                return ClienteDAO.AdicionaCliente(cliente, apiContext);
            }
            return false;
        }
        public static List<ClienteAuxiliar> RetornaClientes(ApiContext apiContext)
        {
            return ClienteDAO.RetornarClientes(apiContext);
        }
        public static ClienteAuxiliar RetornaClientePorCPF(string cpf, ApiContext apiContext)
        {
            return ClienteDAO.RetornaClientePorCPF(cpf,apiContext);
        }

        private static bool Validacoes(Cliente cliente)
        {
            if (!ValidaCPF(cliente.CPF) || !ValidaNome(cliente.Nome) || !ValidaEmail(cliente.Email) || !ValidaTelefone(cliente.Telefone))
            {
                return false;
            }

            return true;

        }
        #region CPF
        /// <summary>
        /// Método retirado do site DevMedia http://www.devmedia.com.br/articles/viewcomp_forprint.asp?comp=3950
        /// </summary>
        /// <param name="vrCPF"></param>
        /// <returns></returns>
        private static bool ValidaCPF(string vrCPF)
        {
            string valor = vrCPF.Replace(".", "");
            valor = valor.Replace("-", "");

            if (valor.Length != 11)

                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
            {
                if (valor[i] != valor[0])
                {
                    igual = false;
                }
            }

            if (igual || valor == "12345678909")
                return false;


            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
            {
                numeros[i] = int.Parse(valor[i].ToString());
            }

            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += (10 - i) * numeros[i];
            }

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }

            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }

            else if (numeros[10] != 11 - resultado)
                return false;

            return true;

        }
        #endregion

        #region Nome
        public static bool ValidaNome(string Nome)
        {
            if (Nome.Trim().Length == 0)
                return false;
            return true;
        }
        #endregion

        #region Email
        /// <summary>
        /// Método retirado do site da Microsoft https://docs.microsoft.com/pt-br/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool ValidaEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        #endregion

        #region Telefone 
        public static bool ValidaTelefone(string Telefone)
        {
            if (Regex.Match(Telefone, @"(\(?\d{2}\)?\s)?(\d{4,5}\-\d{4})").Success)
                return true;
            else
                return false;
        }
        #endregion
    }
}
