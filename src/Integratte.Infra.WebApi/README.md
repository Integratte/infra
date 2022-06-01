# Integratte.Infra.WebApi
O projeto Integratte.Infra.WebApi está relacionado ao projeto Integratte.Infra.

### Controller Api Base
Este projeto contém uma classe base para controllers de API facilitando o tratamento de notificações do módulo "Mediador" de Integratte.Infra e retornando respostas adequadas das requisições com uma resposta web api padrão e o respectivo código de status http.

### Tratamento Global de Erros
Este projeto contém um Middleware para tratamento de erros globais dentro de uma Api.

### Gerar JWT e Configurar Autenticação com JWT
A classe estática JWT possui dois métodos muito úteis. O primeiro configura a autenticação em Apis .Net e a segunda gera um token JWT a partir de claims enviados por um Dictionary<key,value>.