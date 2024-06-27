#Financial System

Este é um projeto ASP.NET Core no padrão MVC que gerencia um sistema financeiro. O sistema permite a criação, visualização, edição e exclusão de faturas (invoices).
Tecnologias Utilizadas

    ASP.NET Core MVC
    Entity Framework Core
    SQL Server

Estrutura do Projeto

    Models: Contém as classes de modelo do domínio.
    Controllers: Contém os controladores que gerenciam a lógica de negócios e manipulam as requisições.
    Views: Contém as páginas de visualização (Razor).
    Data: Contém o contexto do Entity Framework Core e a configuração do banco de dados.

Requisitos

    .NET 6.0 SDK ou superior
    SQL Server

Configuração do Banco de Dados
1. Criar o Banco de Dados

Execute o seguinte script SQL para criar o banco de dados FinancialSystemDB:

sql

CREATE DATABASE FinancialSystemDB;

2. Criar a Tabela Invoices

Execute o seguinte script SQL para criar a tabela Invoices no banco de dados FinancialSystemDB:

sql

-- Criar a tabela Invoices
CREATE TABLE Invoices (
    Id INT PRIMARY KEY IDENTITY,
    PayerName NVARCHAR(100) NOT NULL,
    InvoiceNumber NVARCHAR(50) NOT NULL,
    IssuanceDate DATE NOT NULL,
    BillingDate DATE NOT NULL,
    PaymentDate DATE NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    InvoiceDocument NVARCHAR(100) NOT NULL,
    BankSlipDocument NVARCHAR(100) NOT NULL,
    Status INT NOT NULL
);

-- Verificar os dados da tabela Invoices
SELECT Id, Amount FROM Invoices;

-- Limpando a tabela antes de inserir dados
DELETE FROM Invoices;

-- Inserindo dados mock na tabela Invoices
INSERT INTO Invoices (PayerName, InvoiceNumber, IssuanceDate, BillingDate, PaymentDate, Amount, InvoiceDocument, BankSlipDocument, Status)
VALUES 
('Company A', 'INV-001', '2023-01-10', '2023-01-15', '2023-01-20', 1000.00, 'INV-001.pdf', 'BOL-001.pdf', 3), 
('Company B', 'INV-002', '2023-02-10', '2023-02-15', '2023-02-20', 1500.00, 'INV-002.pdf', 'BOL-002.pdf', 1), 
('Company C', 'INV-003', '2023-03-10', '2023-03-15', '2023-03-20', 2000.00, 'INV-003.pdf', 'BOL-003.pdf', 3), 
('Company D', 'INV-004', '2023-04-10', '2023-04-15', '2023-04-25', 2500.00, 'INV-004.pdf', 'BOL-004.pdf', 2), 
('Company E', 'INV-005', '2023-05-10', '2023-05-15', '2023-05-20', 3000.00, 'INV-005.pdf', 'BOL-005.pdf', 1), 
('Company F', 'INV-006', '2023-06-10', '2023-06-15', '2023-06-20', 3500.00, 'INV-006.pdf', 'BOL-006.pdf', 3), 
('Company G', 'INV-007', '2023-07-10', '2023-07-15', '2023-07-20', 4000.00, 'INV-007.pdf', 'BOL-007.pdf', 1), 
('Company H', 'INV-008', '2023-08-10', '2023-08-15', '2023-08-25', 4500.00, 'INV-008.pdf', 'BOL-008.pdf', 2), 
('Company I', 'INV-009', '2023-09-10', '2023-09-15', '2023-09-20', 5000.00, 'INV-009.pdf', 'BOL-009.pdf', 3), 
('Company J', 'INV-010', '2023-10-10', '2023-10-15', '2023-10-20', 5500.00, 'INV-010.pdf', 'BOL-010.pdf', 1),
('Company K', 'INV-011', '2023-11-10', '2023-11-15', '2023-12-20', 6000.00, 'INV-011.pdf', 'BOL-011.pdf', 3), 
('Company P', 'INV-016', '2024-04-10', NULL, NULL, 11000.00, 'INV-016.pdf', 'BOL-016.pdf', 1), -- TotalAmountNotBilled
('Company Q', 'INV-017', '2024-05-10', NULL, NULL, 12000.00, 'INV-017.pdf', 'BOL-017.pdf', 1), -- TotalAmountNotBilled
('Company R', 'INV-018', '2024-06-10', NULL, NULL, 13000.00, 'INV-018.pdf', 'BOL-018.pdf', 1), -- TotalAmountNotBilled
('Company S', 'INV-019', '2024-07-10', '2024-07-15', '2024-07-20', 14000.00, 'INV-019.pdf', 'BOL-019.pdf', 2), -- TotalAmountDue
('Company T', 'INV-020', '2024-08-10', '2024-08-15', '2024-08-20', 15000.00, 'INV-020.pdf', 'BOL-020.pdf', 2), -- TotalAmountDue
('Company U', 'INV-021', '2024-09-10', '2024-09-15', '2024-09-20', 16000.00, 'INV-021.pdf', 'BOL-021.pdf', 3), -- TotalAmountDue futura
('Company V', 'INV-022', '2024-10-10', '2024-10-15', '2024-10-20', 17000.00, 'INV-022.pdf', 'BOL-022.pdf', 3); -- TotalAmountDue futura

-- Status:
-- 1 = Issued (Emitida)
-- 2 = BillingDone (Cobrança realizada)
-- 3 = PaymentDelayed (Pagamento em atraso)
-- 4 = PaymentReceived (Pagamento realizado)

-- Apagar a tabela Invoices
DROP TABLE dbo.Invoices;

Como Executar o Projeto

    Clone o repositório do projeto:

    bash

git clone https://github.com/usuario/financial-system.git

Navegue até o diretório do projeto:

bash

cd financial-system

Restaure as dependências do projeto:

bash

dotnet restore

Atualize a string de conexão no arquivo appsettings.json com as informações do seu servidor SQL:

json

"ConnectionStrings": {
  "DefaultConnection": "Server=seu_servidor;Database=FinancialSystemDB;User Id=seu_usuario;Password=sua_senha;"
}

Execute as migrações para criar o esquema do banco de dados:

bash

dotnet ef database update

Execute o projeto:

bash

    dotnet run

Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e enviar pull requests.
