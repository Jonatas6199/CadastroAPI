# CadastroAPI

API desenvolvida em ASP.NET CORE com o objetivo de realizar o cadastro de clientes.

Há duas entidades, Cliente e Endereço

O Cliente possui:

- CPF
- Nome
- E-mail
- Telefone
- Endereço

Endereço possui:

- Logradouro
- Número
- Complemento
- Bairro
- Cidade
- Estado

O cliente pode possuir mais de um endereço, defininindo qual o tipo de endereço ele quer cadastrar, como por exemplo, residencial, comercial, cobrança, dentre outros.

Foi utilizado um banco de dados em memória utilizando a biblioteca EntityFrameworkCoreInMemory
