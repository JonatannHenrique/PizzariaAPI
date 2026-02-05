# 🍕 Pizza - API de Gerenciamento

Sistema de backend desenvolvido em **ASP.NET Core** para controle operacional de uma pizzaria, integrando gestão de usuários, cardápio e automação de pedidos.

---

## 🛠️ Tecnologias e Dependências

* **Runtime:** .NET 8.0
* **ORM:** Entity Framework Core (Pomelo MySQL)
* **Banco de Dados:** MySQL
* **Documentação:** Swagger / OpenAPI

---

## 🏗️ Estrutura do Sistema

O projeto utiliza o padrão **MVC** (Model-View-Controller) para organização da lógica:

* **Controllers:** Gerenciam as rotas e regras de negócio (Pedidos, Pizzas, Usuários).
* **Models:** Representam as entidades do banco de dados (`Pizza`, `Cadastro`, `Pedido`).
* **Data (AppDbContext):** Camada de persistência e configuração do banco.
* **DTOs:** Objetos de transferência para garantir respostas rápidas e seguras.

---

## 🚀 Endpoints Principais

### 📦 Pedidos (`/api/Pedido`)
* `GET /`: Lista todos os pedidos registrados.
* `POST /`: Cria um pedido. O sistema calcula automaticamente o **Valor Total** multiplicando a `Quantidade` pelo preço unitário da pizza e define o status como **Pendente** por padrão.
* `PUT /{id}`: Atualiza informações de entrega ou status.

### 🍕 Pizzas (`/api/Pizzas`)
* `GET /`: Retorna o cardápio completo.
* `POST /`: Adiciona novos sabores ao menu.
* `DELETE /{id}`: Remove itens do catálogo.

### 👥 Usuários (`/api/Usuarios`)
* `POST /`: Realiza o cadastro de novos clientes com validação de e-mail único.
* `POST /Login`: Autentica o usuário comparando e-mail e senha.

## 🍇 Persistência de Dados e Configuração

O ecossistema de dados da API foi construído utilizando o **MySQL** como motor de banco de dados, aproveitando a robustez do **Entity Framework Core** para o mapeamento objeto-relacional (ORM).

### 📂 Arquitetura da Camada de Dados
Toda a lógica de infraestrutura está concentrada na pasta:
**Data/**: Onde reside o `AppDbContext.cs`. Este arquivo centraliza a configuração das tabelas e as regras de negócio de persistência para as entidades Pizza, Cadastro (Clientes) e Pedido.

### 🔗 Configuração da String de Conexão
Para conectar a API ao seu servidor local, você deve editar o arquivo `appsettings.json` na raiz do projeto. O formato utilizado é:

**JSON**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=pizzaria_db;Uid=root;Pwd=SUA_SENHA_AQUI;"
}
