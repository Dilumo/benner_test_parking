# Sistema de Controle de Estacionamento

Este projeto é um sistema de controle de estacionamento desenvolvido em C# usando o ASP.NET Core MVC. Ele permite registrar a entrada e saída de veículos, calcular o valor a ser pago com base em uma tabela de preços parametrizável, e armazenar os dados em um banco de dados SQLite usando Entity Framework.

## Funcionalidades

- Registro de entrada de veículos (Placa e Hora)
- Registro de saída de veículos, calculando o valor a pagar
- Cadastro e listagem de preços com controle de vigência
- Utilização da data de entrada do veículo como referência para buscar a tabela de preços
- Interface amigável para registrar entradas, saídas e parametrizações
- Validação de placas de veículos com formatação automática
- Utilização de conceitos de mercado como Entity Framework, LINQ, e MVC

## Estrutura do Projeto

- **Models**: Contém as classes que representam os dados do sistema (VehicleRegistration, PriceTable).
- **Controllers**: Contém as classes que gerenciam a lógica de negócios e a interação com os dados.
- **Views**: Contém as páginas Razor que formam a interface do usuário.
- **Data**: Contém a configuração do contexto do Entity Framework (ParkingContext).

## Requisitos

- .NET 6 SDK
- Banco de dados SQLite

## Como Executar

1. Clone o repositório:
   ```bash
   git clone https://github.com/dilumo/benner_test_parking
   ```
2. Navegue até o diretório do projeto:
   ```bash
   cd Parking
   ```
3. Restaure os pacotes NuGet:
   ```bash
   dotnet restore
   ```
4. Execute as migrações para criar o banco de dados:
   ```bash
   dotnet ef database update
   ```
5. Inicie o aplicativo:
   ```bash
   dotnet run
   ```

## Estrutura do Banco de Dados

- **VehicleRegistrations**: Tabela que armazena as informações de entrada e saída dos veículos.
- **PriceTables**: Tabela que armazena as informações das tabelas de preços com controle de vigência.

## Testes

O projeto inclui testes automatizados para garantir a funcionalidade do sistema. Para rodar os testes:

1. Navegue até o diretório de testes:
   ```bash
   cd TestProject
   ```
2. Execute os testes:
   ```bash
   dotnet test
   ```

Atualmente, a implementação de testes unitários usando o Test-Driven Development (TDD) está planejada para futuras versões.



