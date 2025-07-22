---
name: 
about: 
title: ""
labels: [épico: users, tipo: feature, prioridade: alta]
assignees: ""
---

## 📄 Descrição  
Caso de uso.

---

## 🎯 Objetivo  
Ex: Implementar o fluxo básico de gerenciamento de usuários, permitindo que novos clientes se registrem, visualizem, editem seus dados e desativem sua conta (soft delete).

---

## ✅ Critérios de Aceitação

Ex:
- [ ] É possível realizar o **cadastro** de um novo cliente com nome, email e senha.
- [ ] O sistema permite a **edição** de dados pessoais (nome, senha).
- [ ] O cliente pode **visualizar** seus dados de perfil.
- [ ] O cliente pode **desativar sua conta**, impedindo o login, mas sem apagar os dados.
- [ ] Os dados devem ser validados (ex: email único, senha forte).
- [ ] A autenticação deve ser obrigatória para ações de edição e desativação.
- [ ] O cliente deve receber feedback claro após cada ação (sucesso ou erro).

---

## 🧱 Subtarefas Técnicas

Ex:
🔧 Back-end

Tarefa | Descrição
-- | --
T01 | Criar entidade User com propriedades básicas (Id, Name, Email, Password, IsActive)
T02 | Criar DTOs: CreateUserDto, UpdateUserDto, UserResponseDto
T03 | Implementar CreateUserCommandHandler, UpdateUserCommandHandler, DeactivateUserCommandHandler
T04 | Implementar GetUserByIdQueryHandler
T05 | Criar controller UsersController com endpoints REST (POST, GET, PUT, PATCH)
T06 | Validar regras de negócio: email único, senha criptografada, etc
T07 | Implementar integração com autenticação JWT

🎨 Front-end

Tarefa | Descrição
-- | --
T08 | Criar componente RegisterComponent (formulário de cadastro)
T09 | Criar componente UserProfileComponent (visualização e edição do perfil)
T10 | Criar serviço user.service.ts para consumir a API
T11 | Integrar RegisterComponent com endpoint de criação (/api/users)
T12 | Integrar UserProfileComponent com endpoints de edição e desativação
T13 | Implementar validação de formulário: campos obrigatórios, formato de email, senha mínima
T14 | Mostrar mensagens de sucesso e erro com feedback visual
T15 | Proteger as rotas de perfil com guarda de autenticação (AuthGuard)
T16 | Adicionar loading spinners e desabilitar botões durante requisições
T17 | Realizar testes manuais do fluxo: cadastro > login > perfil > edição > desativação

---