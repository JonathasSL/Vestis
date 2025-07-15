---
name: 🧑‍💼 Manage User
about: Cadastro, edição e desativação de conta do cliente
title: "US01 – Manage User"
labels: [épico: users, tipo: feature, prioridade: alta]
assignees: ""
---

## 📄 Descrição  
Como **novo cliente**, desejo criar minha conta no sistema para acessar os serviços da plataforma Vestis e gerenciar minhas informações pessoais.

---

## 🎯 Objetivo  
Implementar o fluxo básico de gerenciamento de usuários, permitindo que novos clientes se registrem, visualizem, editem seus dados e desativem sua conta (soft delete).

---

## ✅ Critérios de Aceitação

- [ ] É possível realizar o **cadastro** de um novo cliente com nome, email e senha.
- [ ] O sistema permite a **edição** de dados pessoais (nome, senha).
- [ ] O cliente pode **visualizar** seus dados de perfil.
- [ ] O cliente pode **desativar sua conta**, impedindo o login, mas sem apagar os dados.
- [ ] Os dados devem ser validados (ex: email único, senha forte).
- [ ] A autenticação deve ser obrigatória para ações de edição e desativação.
- [ ] O cliente deve receber feedback claro após cada ação (sucesso ou erro).

---

## 🧱 Subtarefas Técnicas

| Tarefa | Tipo | Repositório |
|--------|------|-------------|
| Criar entidade `User` e `UserDto` | back-end | vestis-backend |
| Criar `CreateUserCommand`, `UpdateUserCommand`, `DeactivateUserCommand` | back-end | vestis-backend |
| Criar controller e endpoints (`POST`, `GET`, `PUT`, `PATCH`) | back-end | vestis-backend |
| Criar componente de formulário de cadastro | front-end | vestis-frontend |
| Criar componente de perfil de usuário | front-end | vestis-frontend |
| Implementar serviço de autenticação com JWT | ambos | vestis-backend + vestis-frontend |
| Criar validações de formulário (front) e regras de negócio (back) | ambos | — |
| Testar fluxo completo: cadastro > login > editar > desativar | ambos | — |

---

## 🧩 Épico Relacionado  
🔗 `User Management`

---

## 🕓 Sprint Sugerida  
Sprint 1

---

## 🏷️ Labels sugeridas  
- `épico: users`  
- `tipo: feature`  
- `repo: [backend,frontend]`
- `prioridade: alta`  
- `sprint: 1`
