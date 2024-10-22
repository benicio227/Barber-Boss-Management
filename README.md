## Sobre o projeto

A **API** **Barber Boss Management** é um sistema de gerenciamento para barbearias desenvolvido em **.NET 8**, utilizando uma arquitetura em camadas e seguindo o conceito de **Domain Driven Design (DDD)**. O projeto permite que os proprietários de barbearias realizem login, cadastrem suas barbearias e registrem faturamentos mensais de forma simples e eficaz.

As principais funcionalidades incluem operações **CRUD** para usuários, barbearias e faturamentos. A estrutura do banco de dados é composta por três tabelas: Revenue (faturamento), que está vinculada a uma barbearia; Barbearia, que é associada a um usuário; e a tabela de usuários do sistema.

Os dados são armazenados em um banco de dados **MySQL**, e a arquitetura do projeto é baseada em **REST**, proporcionando uma interação eficiente entre o cliente e o servidor. A API conta com documentação gerada automaticamente pelo **Swagger**, facilitando a integração e testes por parte dos desenvolvedores, além de tornar a exploração da API mais intuitiva. Para facilitar o desenvolvimento, foram utilizados pacotes NuGet como **AutoMapper**, responsável pelo mapeamento entre objetos de domínio e requisições/respostas, reduzindo a necessidade de código repetitivo; **FluentValidation**, que implementa regras de validação de forma simples e intuitiva nas classes de requisições, mantendo o código limpo e fácil de manter; e **FluentAssertions**, utilizado nos testes de unidade para tornar as verificações mais legíveis e ajudar a escrever testes claros e compreensíveis. Além disso, o **Entity Framework** atua como um ORM(Object-Relational Mapper) que simplifica as interações com o banco de dados, permitindo o uso de objetos .NET para manipular dados diretamente, sem a necessiade de lidar com consultas SQL.

### Features

- **Domain Driven Design (DDD)**: Estrutura modular que facilita o entendimento e a manutenção do domínio da aplicação.
- **Testes de Unidade**: Testes abrangentes com FluentAssertions para garantir a funcionalidade e a qualidade.
- **Geração de Relatórios**: Capacidade de exportar relatórios detalhados para **PDF** e **Excel**, oferecendo uma análise visual e eficaz das despesas.
- **RESTful API com Documentação Swagger**: Interface documentada que facilita a integração e o teste por parte dos desenvolvedores. 

## Getting Started

Para obter uma cópia local funcionando, siga estes passos simples.

### Requisitos

- Visual Studio versão 2022+ ou Visual Studio Code
- Windows 10+ ou Linux/MacOS com [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) instalado
- MySql Server

### Instalação

1. Clone o repositório:
    ```sh
    git clone git@github.com:benicio227/Barber-Boss-Management.git
    ```

2. Preencha as informações no arquivo `appsettings.Development.json`.
3. Execute a API e aproveite o seu teste