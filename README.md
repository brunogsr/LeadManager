# Lead Management SPA

Este é um projeto Full Stack de exemplo para um desafio técnico, criando uma interface de usuário de gerenciamento de leads (SPA - Single Page Application) para uma empresa fictícia.

## Descrição

A aplicação permite visualizar leads de vendas em duas categorias principais: "Invited" (Convidados/Novos) e "Accepted" (Aceitos). Usuários podem aceitar ou recusar leads na categoria "Invited". Ao aceitar um lead com preço acima de $500, um desconto de 10% é aplicado automaticamente. Uma notificação simulada por email é gerada (via console e log em arquivo) quando um lead é aceito.

O backend é construído com ASP.NET Core Web API, usando Entity Framework Core e um banco de dados SQLite. O frontend é uma SPA construída com React e TypeScript, utilizando Vite como ferramenta de build.

## Funcionalidades Principais

*   **Visualização de Leads:** Exibe leads em abas separadas ("Invited" e "Accepted").
*   **Aceitar Leads:** Permite aceitar leads do status "Invited", movendo-os para "Accepted".
*   **Recusar Leads:** Permite recusar leads do status "Invited", removendo-os da visualização principal (status alterado para "Declined" no backend).
*   **Lógica de Desconto:** Aplica automaticamente um desconto de 10% a leads aceitos se o preço original for superior a $500.
*   **Criação de Leads (via API):** Endpoint `POST /api/lead` permite adicionar novos leads (requer um `JobId` válido existente).
*   **Notificação Simulada:** Loga no console e em um arquivo (`email_log.txt`) quando um lead é aceito, simulando um email para "vendas@test.com".
*   **Backend API RESTful:** Fornece os dados e a lógica de negócio.
*   **Frontend SPA React:** Interface reativa para interação do usuário.

## Tecnologias Utilizadas

**Backend:**

*   **Framework:** ASP.NET Core 8 (ou 6 - ajuste conforme sua versão)
*   **Linguagem:** C#
*   **ORM:** Entity Framework Core 8 (ou compatível com seu SDK .NET)
*   **Banco de Dados:** SQLite
*   **Arquitetura:** API RESTful com padrão Controller-Service

**Frontend:**

*   **Framework/Lib:** React 19 (ou sua versão)
*   **Linguagem:** TypeScript
*   **Build Tool:** Vite
*   **Requisições HTTP:** Axios
*   **Estilização:** CSS Padrão

## Pré-requisitos

Antes de começar, garanta que você tenha instalado:

*   **SDK do .NET:** Versão 8 ou 6 (LTS) - Verifique com `dotnet --version`
*   **Node.js e npm/yarn:** Node.js versão 18 ou superior é recomendado. (Verifique com `node -v` e `npm -v` ou `yarn -v`)
*   **Git:** Para clonar o repositório.
*   **IDE/Editor de Código:**
    *   Visual Studio 2022 (para backend .NET) ou
    *   Visual Studio Code (com extensões para C# e React/TypeScript)

## Configuração e Instalação

Siga estes passos para configurar o ambiente de desenvolvimento local:

1.  **Clonar o Repositório:**
    ```bash
    git clone <URL_DO_SEU_REPOSITORIO>
    cd <NOME_DA_PASTA_RAIZ_DO_PROJETO>
    ```

2.  **Configurar o Backend (.NET API):**
    *   Navegue até a pasta do backend (ex: `LeadManager`):
        ```bash
        cd LeadManager
        ```
    *   Restaure as dependências do .NET:
        ```bash
        dotnet restore
        ```
    *   **IMPORTANTE:** Aplique as migrações do Entity Framework para criar o banco de dados SQLite (`LeadManager.db`) e popular com dados iniciais (seed):
        ```bash
        dotnet ef database update
        ```
        *(Se encontrar erros, pode ser necessário remover migrações anteriores ou o arquivo `.db` e tentar novamente)*

3.  **Configurar o Frontend (React App):**
    *   Navegue até a pasta do frontend (ex: `leadmanager-frontend`):
        ```bash
        cd ../leadmanager-frontend
        # (ou o caminho correto a partir da raiz do projeto)
        ```
    *   Instale as dependências do Node.js:
        ```bash
        npm install
        # ou, se você usa yarn:
        # yarn install
        ```
    *   **⚠️ CRÍTICO:** Atualize a URL da API no frontend.
        *   Abra o arquivo `leadmanager-frontend/src/services/apiService.ts`.
        *   Localize a constante `API_BASE_URL`.
        *   **Altere o valor** para corresponder à URL **HTTP** em que sua API .NET está rodando (você verá essa URL no terminal ao iniciar o backend, ex: `http://localhost:5150`). Exemplo:
            ```typescript
            // src/services/apiService.ts
            const API_BASE_URL = 'http://localhost:5150/api/lead'; // <-- AJUSTE SE NECESSÁRIO
            ```

## Executando a Aplicação

Você precisa executar o backend e o frontend simultaneamente.

1.  **Iniciar o Backend (.NET API):**
    *   Abra um terminal na pasta do backend (`LeadManager`).
    *   Execute o comando:
        ```bash
        dotnet run
        ```
    *   Observe a saída no terminal. Ele indicará a URL em que a API está escutando (ex: `Now listening on: http://localhost:5150`). Anote esta URL (ela deve corresponder à `API_BASE_URL` configurada no frontend).
    *   **Este terminal deve permanecer aberto enquanto você usa a aplicação.**

2.  **Iniciar o Frontend (React App):**
    *   Abra **outro** terminal na pasta do frontend (`leadmanager-frontend`).
    *   Execute o comando:
        ```bash
        npm run dev
        # ou, se você usa yarn:
        # yarn dev
        ```
    *   O Vite iniciará o servidor de desenvolvimento e, graças à flag `--open` no `package.json`, deve abrir automaticamente uma aba no seu navegador padrão apontando para o endereço do frontend (geralmente `http://localhost:5173`).

3.  **Acessar a Aplicação:**
    *   Use a aplicação na aba do navegador que o Vite abriu (`http://localhost:5173` ou similar).

## Estrutura do Projeto (Simplificada)
