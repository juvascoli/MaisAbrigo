# 🛟 MaisAbrigo API

API REST para gerenciamento de pessoas resgatadas e abrigos em situações de emergência. O projeto combina uma **plataforma web**, **tecnologia IoT** e **dashboard analítico** para apoiar a **Defesa Civil**, voluntários e cidadãos em momentos críticos.

---

## 🚀 Funcionalidades Principais

### 👥 Cadastro de Pessoas Resgatadas
- Nome
- Idade
- Condições de saúde
- Associadas a um abrigo (relacionamento 1:N)

### 🏠 Cadastro e Consulta de Abrigos
- Nome
- Endereço
- Recursos disponíveis (comida, água, leito)
- Ocupação atual

---

## 📲 Integrações e Inovações


### 📡 IoT (Internet das Coisas)
- Dispositivos instalados nos abrigos para:
  - **Sensor de presença** (entrada/saída de pessoas) → atualiza a ocupação
  - **Sensor de temperatura ambiente**
- Integração via **MQTT** com **Node-RED** ou **Thinger.io**

### 📊 Dashboard para Defesa Civil
- Visualiza:
  - Ocupação de cada abrigo
  - Temperatura ambiente em tempo real
  - Número de abrigados
- Desenvolvido com **Node-RED** ou **Thinger.io**

---

## 🧱 Tecnologias Utilizadas

| Camada       | Tecnologia                         |
|--------------|-------------------------------------|
| Backend API  | ASP.NET Core MVC                   |
| Banco de Dados | Oracle (via EF Core)             |
| ORM          | Entity Framework Core (Code-First) |
| IoT          | Node-RED, MQTT, Arduino / ESP8266  |
| Dashboard    | Node-RED ou Thinger.io             |
| Documentação | Swagger / OpenAPI                  |
| Views        | Razor Pages + TagHelpers           |

---

## 🗄️ Estrutura do Projeto

- `Model`: Entidades Abrigo e Pessoa
- `DTOs`: Entrada e resposta via API
- `Controllers`: Endpoints RESTful
- `DbContext`: Configuração EF Core com relacionamento 1:N
- `Swagger`: Interface interativa da API

---

## 🧪 Endpoints REST (exemplos)

| Verbo | Rota                    | Ação                        |
|-------|-------------------------|-----------------------------|
| GET   | `/api/abrigo`           | Lista todos os abrigos      |
| POST  | `/api/abrigo`           | Cria um novo abrigo         |
| GET   | `/api/pessoa`           | Lista pessoas resgatadas    |
| POST  | `/api/pessoa`           | Cadastra uma nova pessoa    |

---

## 🧭 Como Rodar o Projeto

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- Banco Oracle ou SQL Server (local ou Docker)

### Comandos principais
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run