
# Lead Manager Challenge

Repositório: [https://github.com/brunogsr/LeadManager](https://github.com/brunogsr/LeadManager)

Aplicação Full Stack (SPA) para gerenciamento de leads de vendas, construída como parte de um desafio técnico.

## Tecnologias Utilizadas

*   **Backend:**
    *   .NET 8
    *   ASP.NET Core Web API
    *   Entity Framework Core 9.0.4 (Preview)
    *   SQLite
    *   C#
*   **Frontend:**
    *   React 19
    *   TypeScript (~5.7.2)
    *   Vite 6.3.1
    *   Axios
    *   CSS
*   **Banco de Dados:** SQLite

## Pré-requisitos

*   [.NET SDK 8](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [Node.js](https://nodejs.org/) (v18 ou superior recomendado) e npm
*   [Git](https://git-scm.com/)

## Configuração

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/brunogsr/LeadManager.git
    cd LeadManager
    ```

2.  **Configure o Backend:**
    *   Navegue até a pasta do backend (que é a raiz do projeto clonado):
        ```bash
        # Você já deve estar na pasta LeadManager após o cd anterior
        ```
    *   Restaure as dependências .NET:
        ```bash
        dotnet restore
        ```
    *   Crie/Atualize o banco de dados SQLite (`LeadManager.db`):
        ```bash
        dotnet ef database update
        ```

3.  **Configure o Frontend:**
    *   Navegue até a pasta do frontend:
        ```bash
        cd leadmanager-frontend
        ```
    *   Instale as dependências Node.js:
        ```bash
        npm install
        ```
    *   **⚠️ Ajuste a URL da API:**
        *   Abra o arquivo `leadmanager-frontend/src/services/apiService.ts`.
        *   Verifique a constante `API_BASE_URL`.
        *   **Certifique-se que ela corresponda à URL HTTP** exibida quando você iniciar o backend (geralmente `http://localhost:5150/api/lead`).

## Execução

Execute o backend e o frontend simultaneamente em terminais separados:

1.  **Iniciar Backend (.NET API):**
    *   No terminal na pasta raiz (`LeadManager`):
        ```bash
        dotnet run
        ```
    *   Observe a URL informada (ex: `http://localhost:5150`). **Mantenha este terminal aberto.**

2.  **Iniciar Frontend (React App):**
    *   Abra **outro terminal** na pasta do frontend (`LeadManager/leadmanager-frontend`).
    *   Execute:
        ```bash
        npm run dev
        ```
    *   A aplicação frontend será aberta automaticamente no seu navegador (geralmente `http://localhost:5173`).

## Endpoints da API (Principais)

*   `GET /api/lead/invited`: Lista leads convidados.
*   `GET /api/lead/accepted`: Lista leads aceitos.
*   `POST /api/lead`: Cria um novo lead (requer `JobId` válido existente).
*   `POST /api/lead/{id}/accept`: Aceita o lead `{id}`.
*   `POST /api/lead/{id}/decline`: Recusa o lead `{id}`.

## Notas Importantes

*   **Email:** O envio de email é **simulado**. Verifique o **console da API .NET** (terminal onde `dotnet run` está executando) ou o arquivo `LeadManager/email_log.txt` para ver as notificações após aceitar um lead.
*   **Criação de Leads:** Para criar um lead via `POST /api/lead`, o `JobId` fornecido no corpo JSON deve ser um `Id` que já existe na tabela `Jobs` (IDs 1 a 5 estão no seed inicial). A API retornará erro 400 se o `JobId` for inválido.
