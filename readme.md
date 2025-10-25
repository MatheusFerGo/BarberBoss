# BarberBoss API - Projeto de Faturamento

<p align="left">
  <a href="https://github.com/SEU-USUARIO/SEU-REPO/actions/workflows/build.yml">
    <img alt="Build Status" src="https://img.shields.io/github/actions/workflow/status/SEU-USUARIO/SEU-REPO/.github/workflows/build.yml?branch=main&style=for-the-badge">
  </a>
</p>

<p align="left">
  <img alt=".NET 9" src="https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white">
  <img alt="MySQL" src="https://img.shields.io/badge/MySQL-(Docker)-4479A1?style=for-the-badge&logo=mysql&logoColor=white">
  <img alt="Tests (xUnit)" src="https://img.shields.io/badge/Tests-xUnit-007abf?style=for-the-badge&logo=xunit&logoColor=white">
  <img alt="Docker" src="https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white">
</p>

API RESTful para o projeto "BarberBoss", um sistema de gerenciamento de faturamento (Billing) para uma barbearia. Este projeto foi desenvolvido como um estudo aprofundado de arquiteturas de software, seguindo os princípios de Domain-Driven Design (DDD) e SOLID.

## 🚀 Funcionalidades (Release 1.0)

* **CRUD de Faturamento**: Gerenciamento completo (Criar, Ler, Atualizar, Excluir) de lançamentos de faturamento.
* **Relatórios Mensais**: Geração de relatórios de faturamento mensais nos formatos **PDF** e **Excel**.
* **Regras de Negócio**: O cálculo do total nos relatórios considera apenas faturamentos com status "Pago".
* **Validações de Domínio**: A entidade `Billing` protege as regras de negócio (ex: valor não pode ser negativo, faturamento cancelado deve ter valor zero).
* **Tratamento Global de Erros**: Um middleware personalizado captura todas as exceções e retorna respostas JSON amigáveis e padronizadas (Status 500, 400, 404).
* **Testes de Unidade**: Cobertura de testes para as regras de negócio críticas (Domain) e cálculos (Application).

## 🛠️ Arquitetura e Tecnologias

Este projeto foi construído utilizando uma arquitetura limpa (Clean Architecture) baseada nos padrões do **Domain-Driven Design (DDD)** e **SOLID**.

* **`BarberBoss.Domain`**: Camada mais interna. Contém as Entidades, Enums e as interfaces dos Repositórios. Não depende de nenhuma outra camada.
* **`BarberBoss.Application`**: Camada de orquestração. Contém os DTOs, interfaces de Serviços e a lógica dos *use cases* (ex: `ReportService`).
* **`BarberBoss.Infrastructure`**: Camada de implementação. Contém as classes concretas que interagem com ferramentas externas, como o `DbContext` (Entity Framework), o `BillingRepository`, e os serviços `ExcelService` (ClosedXML) e `PdfService` (QuestPDF).
* **`BarberBoss.Api`**: Camada de apresentação. Expõe a aplicação para o mundo externo via endpoints REST. Contém os Controllers, Middlewares e a configuração de Injeção de Dependência.
* **`BarberBoss.Domain.Tests` / `Application.Tests`**: Projetos de teste de unidade.

### Tecnologias Principais

| Categoria | Tecnologia |
| :--- | :--- |
| **Backend** | C# (.NET) |
| **Arquitetura** | Domain-Driven Design (DDD), SOLID, Clean Architecture |
| **API** | RESTful API (ASP.NET Core) |
| **Banco de Dados** | MySQL (executando em Docker) |
| **ORM** | Entity Framework Core |
| **Testes** | xUnit e Moq |
| **Relatórios** | QuestPDF (para PDF), ClosedXML (para Excel) |
| **Documentação** | Swagger (OpenAPI) |
| **Contêiner** | Docker (Docker Compose) |
| **Workflow** | Gitflow |

---

## 🏁 Como Rodar o Projeto

Siga os passos abaixo para executar a aplicação localmente.

### Pré-requisitos

* [.NET SDK](https://dotnet.microsoft.com/en-us/download)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)
* [Git](https://git-scm.com/downloads)

### 1. Clonar o Repositório

```bash
git clone https://[URL-DO-SEU-REPOSITORIO-GIT]
cd BarberBoss
```

### 2. Configurar Variáveis de Ambiente (Segurança)

Este projeto usa **User Secrets** (para o .NET) e um arquivo **`.env`** (para o Docker) para proteger a senha do banco de dados. A senha deve ser a mesma em ambos.

**a) Configurar o `.env` (para o Docker)**

1.  Na raiz do projeto (`/BarberBoss/`), crie um arquivo chamado `.env`
2.  Adicione a seguinte linha a ele, definindo sua senha:
    ```
    MYSQL_PASSWORD=SuaSenhaForteAqui
    ```
    *(O `.gitignore` já está configurado para ignorar este arquivo).*

**b) Configurar o `User Secrets` (para o .NET)**

1.  Execute o comando `init` no terminal, na raiz do projeto:
    ```bash
    dotnet user-secrets init -p BarberBoss.Api
    ```
2.  Configure a Connection String para usar a **mesma senha** do passo `a)`:
    ```bash
    dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=barberboss_db;User=root;Password=SuaSenhaForteAqui" -p BarberBoss.Api
    ```

### 3. Iniciar o Banco de Dados (Docker)

1.  Certifique-se de que o Docker Desktop está **em execução**.
2.  Execute o Docker Compose para criar e iniciar o contêiner do MySQL:
    ```bash
    docker-compose up -d
    ```

### 4. Aplicar as Migrações (Criar as Tabelas)

Com o banco de dados rodando (aguarde alguns segundos), vamos criar as tabelas:

```bash
# Aponta para o projeto de startup (Api) e o de infra (DbContext)
dotnet ef database update -p BarberBoss.Infrastructure -s BarberBoss.Api
```

### 5. Executar a API

```bash
dotnet run --project BarberBoss.Api
```