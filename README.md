# Integratte.Infra
Código reutilizável para camada de infra de aplicações .NET. Verificar usos nos projetos de testes deste repositório.
Nuget: Install-Package Integratte.Infra

### Classes de Tipo
Módulo que contém classes para tipos específicos como CPF, CNPJ, E-mail, etc. Ao utilizar uma classe de tipo, não precisamos nos preocupar com questões de validação e também trazemos mais robustes para o sistema.

### E-mail
Módulo que contém uma abstração de envio de e-mail e uma implementação utilizando System.Net e carregamento das configurações de um arquivo de configuração de e-mail.

### Exceções Personalizadas
Módulo que contém excessões gerenciadas na camada de infra que são utilizadas para tratamentos específicos como uma situação provacada por erro de programação ou um problema na comunicação com uma API específica.

### Extensões
Módulos que extende classes como String, Exception, Enum, etc. Os métodos extendidos facilitam a manutenção e clareza do código com uma linguagem coerente e um encapsulamento que simplifica diversos processos.

# Outros projetos associados

## Mediador
Módulo que facilita a execução/publicação de comandos, consultas, eventos e notificações conforme o padrão Mediator e Observer. 
Este módulo possui sua abstração no projeto Integratte.Infra e sua implementação no projeto Integratte.Infra.MediatR, pois sua implementação utiliza a biblioteca MediatR para a lógica dos padrões Mediator e Observer. Para unir o Mediador Integratte com a biblioteca MediatR foi utilizado o padrão Adapter.
Nuget: Install-Package Integratte.Infra.MediatR

## Web Api
Módulo implementado no projeto Integratte.Infra.WebApi com uma classe base para controllers de API facilitando o tratamento de notificações e respostas adequadas das requisições.
Nuget: Install-Package Integratte.Infra.MediatR

## Http
No projeto Integratte.Infra há uma abstração de chamadas http que é implementada no projeto Integratte.Infra.RestSharp, pois utilizamos a biblioteca RestSharp para gerenciar as chamadas Http abstraídas.
Nuget: Install-Package Integratte.Infra.RestSharp

## Entity Framework
Extensões e abstrações no desenvolvimento de banco de dados CodeFirst utilizando o EntityFramework.
Nuget: Install-Package Integratte.Infra.EntityFramework

## Cache
Abstrações e implementações para gerenciamentos de cache.
Nuget: Install-Package Integratte.Infra.Cache