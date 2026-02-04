🍕 Pizza API
API robusta desenvolvida em ASP.NET Core para o gerenciamento de uma pizzaria, permitindo o controle de usuários, cardápio de pizzas e processamento de pedidos em tempo real.

🚀 Tecnologias Utilizadas
Framework: .NET 8.0 (C#)

Banco de Dados: MySQL (via Entity Framework Core)

Documentação: Swagger (OpenAPI)

Arquitetura: MVC (Controllers), DTOs e Repository Pattern

Segurança: Configuração de CORS para integração com Front-end

🛠️ Estrutura do Projeto
O projeto está organizado para facilitar a manutenção e escalabilidade:

Controllers/: Endpoints da API (Pedidos, Pizzas, Usuários).

Models/: Classes que representam as tabelas do banco de dados.

Data/: Contexto do Banco de Dados (AppDbContext).

DTOs/: Objetos de Transferência de Dados para respostas otimizadas.

📌 Endpoints Principais
📦 Pedidos (/api/Pedido)
GET /api/Pedido: Lista todos os pedidos realizados.

POST /api/Pedido: Cria um novo pedido (calcula automaticamente o valor total com base na pizza selecionada).

PUT /api/Pedido/{id}: Atualiza status ou informações do pedido.

DELETE /api/Pedido/{id}: Remove um pedido do sistema.

🍕 Pizzas (/api/Pizzas)
GET /api/Pizzas: Retorna o cardápio completo.

POST /api/Pizzas: Adiciona uma nova opção de sabor e preço.

PUT /api/Pizzas/{id}: Edita informações de uma pizza existente.

👥 Usuários (/api/Usuarios)
GET /api/Usuarios: Lista todos os clientes cadastrados.

POST /api/Usuarios: Realiza o cadastro de um novo usuário.

POST /api/Usuarios/Login: Autentica o usuário no sistema.
