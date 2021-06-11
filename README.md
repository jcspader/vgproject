# CRUD Test
![alt text](https://github.com/jcspader/vgproject/blob/master/doc/Main.JPG?raw=true)


A solução do projeto foi estruturado segundo o padrão DDD e tem a seguinte configuração:

![alt text](https://github.com/jcspader/vgproject/blob/master/doc/Solution.JPG?raw=true)

## Tecnologias utilizadas
- C# .NET ASP.NET WebApi 	- Projeto Backend
- C# .NET ASP.NET SPA React - Projeto FontEnd

- Entity Framework Core (https://docs.microsoft.com/pt-br/ef/core/)
- AutoMapper (https://automapper.org/)
- Microsoft Injection Dependency
- Fluent Migrations (https://fluentmigrator.github.io/)
- NUnit
- React JS
- Bootstrap
- SQLLite

## Padrões utilizados

- Domain Domain Driven Design
- Repository Design
- ORM
- API
- Front End e BackEnd independentes


## Cobertura em Testes
![alt text](https://github.com/jcspader/vgproject/blob/master/doc/coverage.JPG?raw=true)


## Banco de Dados
Banco de dados utilizado SqlLite.
o caminho padrão para armazenamento é Projects\VG.WebApi\Database\VG.db que é criado automaticamente na inicialização.
A configuração de caminho está presente no arquivo appsettings.json do projeto VG.WebApi.
Para visualizar os dados, você pode utilizar a ferramenta DB Browser for SQLite (https://sqlitebrowser.org/dl/) disponivel para Windows, MacOS e Linux.


## Setup
1. Clone do Projeto
2. Abrir a Solution no Visual Studio (pasta Solution) 
3. Ajusta para inicialização em multiplos projetos:
	3.1. Clique com botão direito na Solução e logo em seguida "Set Startup Projects"
	![alt text](https://github.com/jcspader/vgproject/blob/master/doc/Step1.JPG?raw=true)
	
	3.2. Maque os projetos "VG.WebApp" e "VG.WebApi" com ação Start.
	![alt text](https://github.com/jcspader/vgproject/blob/master/doc/Step2.JPG?raw=true)	

4. Compilar e Executar Projetos no Visual Studio
