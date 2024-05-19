# Baixa a imagem docker do SQLServer 
- mcr.microsoft.com/mssql/server:2022-latest
- Após executa-la em um container docker, acesso sua instancia através do Azure Data Studio ou SQL Management Studio e, exucute os scripts abaixo

   CREATE TABLE [dbo].[User] (
    Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(256),
    ClientId VARCHAR(256),
    Secret VARCHAR(256),
    CreatedAt DATETIME 
);

# Authentication

# No endpoint NewUser, informe as credencias de acesso, que devem ser cadastradas, conforme exemplo abaixo:

Name - Teste

Secret - 95b4f998-8400-4c46-8d45-588b4f6d7d17

Client - 1Vjyl3Q5yEqsO2L-ZBVMmg

# Após a solicitação ter sido efetuada, voce deve solicitar um token através do EndPoint 'Auth', do qual deve ser utilizado para se autenticar na API https://github.com/Shonuvik/FinancialManagementAPI/tree/develop 


