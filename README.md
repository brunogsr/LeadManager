# Lead Manager Challenge 🚀

**Repositório:** [https://github.com/brunogsr/LeadManager](https://github.com/brunogsr/LeadManager)

Aplicação Full Stack (SPA) para gerenciamento de leads de vendas, construída como parte de um desafio técnico.

---

## 🎯 Visão Geral do Projeto

Este projeto simula um sistema onde leads de vendas são recebidos e processados. A interface principal (frontend) é dividida em duas abas:

*   **Invited:** Exibe todos os leads que acabaram de chegar e aguardam uma ação (**Aceitar** ou **Recusar**).
*   **Accepted:** Exibe os leads que foram aceitos pela equipe.

A aplicação também inclui lógica de negócio como aplicação de desconto (10% para leads > $500) e simulação de notificação por email ao aceitar um lead.

---

## 🛠️ Tecnologias Utilizadas

**Backend:**
*   .NET 8
*   ASP.NET Core Web API
*   Entity Framework Core 9.0.4 (Preview)
*   SQLite
*   C#

**Frontend:**
*   React 19
*   TypeScript (~5.7.2)
*   Vite 6.3.1
*   Axios
*   CSS

**Banco de Dados:**
*   SQLite

---

## ✅ Pré-requisitos

*   [.NET SDK 8](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [Node.js](https://nodejs.org/) (v18 ou superior recomendado) e npm
*   [Git](https://git-scm.com/)

---

## ⚙️ Configuração e Instalação

Siga os passos para rodar o projeto localmente:

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/brunogsr/LeadManager.git
    cd LeadManager
    ```

2.  **Configure o Backend:**
    *   (Você já estará na pasta `LeadManager`)
    *   Restaure dependências .NET:
        ```bash
        dotnet restore
        ```
    *   Crie/Atualize o banco de dados SQLite (`LeadManager.db`):
        ```bash
        dotnet ef database update
        ```
        > **Nota:** Se o comando acima falhar com "comando não encontrado", instale as ferramentas EF Core globalmente:
        > ```bash
        > dotnet tool install --global dotnet-ef
        > ```
        > Depois, **feche e reabra seu terminal** e tente `dotnet ef database update` novamente.

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

---

## ▶️ Execução

Execute o backend e o frontend **simultaneamente** em terminais separados:

1.  **Iniciar Backend (.NET API):**
    *   Abra um **Terminal 1** na pasta raiz (`LeadManager`).
    *   Execute:
        ```bash
        dotnet run
        ```
    *   Observe a URL informada (ex: `http://localhost:5150`). **Mantenha este terminal aberto.**

2.  **Iniciar Frontend (React App):**
    *   Abra um **Terminal 2** na pasta do frontend (`LeadManager/leadmanager-frontend`).
    *   Execute:
        ```bash
        npm run dev
        ```
    *   A aplicação frontend será aberta automaticamente no seu navegador (geralmente `http://localhost:5173`).

---

## ✨ Funcionalidades e Testes

A API não requer autenticação. Use a interface do frontend ou ferramentas como Postman.
### Interface do Usuário (Frontend)

*   Acesse `http://localhost:5173` (ou a porta informada pelo Vite).
*   **Abas:** Alterne entre "Invited" e "Accepted".
*   **Cards "Invited":**
    *   Exibem: Nome, Data, 📍 Localização, 💼 Categoria, Job ID, Descrição, Preço.
    *   Possuem botões **Accept** (laranja) e **Decline** (cinza).
*   **Cards "Accepted":**
    *   Exibem: Nome Completo, Data, 📍 Localização, 💼 Categoria, Job ID, 📞 Telefone, ✉️ Email, Descrição, Preço Final.

### Ações Principais

*   **Aceitar Lead (Botão "Accept"):**
    1.  Move o lead para a aba "Accepted".
    2.  Aplica desconto de 10% se Preço > $500.
    3.  **Gera Email Simulado:** Verifique o **console do Terminal 1 (backend)** ou o arquivo `LeadManager/email_log.txt`.
*   **Recusar Lead (Botão "Decline"):**
    1.  Remove o lead da aba "Invited". (Status no DB vira "Declined").

### Testando a API Diretamente (Ex: Postman)

*   **URL Base:** `http://localhost:5150` (ou a porta do seu backend)

*   **`GET /api/lead/invited`**
    *   _Função:_ Lista leads convidados.
    *   _Teste:_ Comparar com a aba "Invited" no frontend.

*   **`GET /api/lead/accepted`**
    *   _Função:_ Lista leads aceitos.
    *   _Teste:_ Comparar com a aba "Accepted" no frontend.

*   **`POST /api/lead`**
    *   _Função:_ Cria um novo lead (status "Invited").
    *   _Requisito:_ `jobId` no corpo JSON deve ser um ID válido da tabela `Jobs` (1-5 do seed).
    *   _Teste:_ Enviar JSON, verificar resposta `201 Created`, atualizar frontend para ver o novo lead.
        ```json
        // Exemplo Body (Use JobId 1, 2, 3, 4 ou 5)
        {
          "firstName": "API Test",
          "lastName": "Lead",
          "suburb": "Postman City 9000",
          "category": "API Testing",
          "jobId": 2, // <-- ID de Job Válido
          "description": "Creating lead via API.",
          "price": 75.00,
          "email": "api@test.xyz"
        }
        ```

*   **`POST /api/lead/{id}/accept`**
    *   _Função:_ Aceita o lead, aplica desconto, simula email.
    *   _Teste:_ Usar ID de lead "Invited", verificar mudança de status (`GET /invited` vs `GET /accepted`), verificar preço (se > $500), verificar log/console do email.

*   **`POST /api/lead/{id}/decline`**
    *   _Função:_ Recusa o lead.
    *   _Teste:_ Usar ID de lead "Invited", verificar se some de `GET /invited`.

---

## 📝 Notas Adicionais

*   **Persistência:** Os dados são salvos no arquivo `LeadManager/LeadManager.db`.
*   **Job ID:** Cada lead está vinculado a um Job (trabalho/serviço) existente no banco de dados.
*   **Email Simulado:** Nenhuma configuração de email real é necessária. As notificações são apenas logs.

---
