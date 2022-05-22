# Integratte.Infra
Código reutilizável para camada de infra de aplicações .NET

## Mediador
Módulo que facilita a execução/publicação de comandos, consultas, eventos e notificações conforme o padrão Mediator e Observer. 
Este módulo possui sua abstração no projeto Integratte.Infra e sua implementação no projeto Integratte.Infra.MediatR, pois sua implementação utiliza a biblioteca MediatR para a lógica dos padrões Mediator e Observer. Para unir o Mediador Integratte com a biblioteca MediatR foi utilizado o padrão Adapter.

## Web Api
Módulo com uma classe base para controllers de API facilitando o tratamento de notificações e respostas adequadas das requisições.