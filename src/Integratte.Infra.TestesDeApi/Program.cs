using Integratte.Infra.TestesDeApi;
using Integratte.Infra.WebApi.TratamentoDeErrosGlobais;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Integratte.Infra.WebApi.JWT;
using Microsoft.OpenApi.Models;
using Integratte.Infra.TestesDeApi.ModuloHttp;
using Integratte.Infra.ModuloHttp;

var builder = WebApplication.CreateBuilder(args);

#region Customizado - Builder

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

});

builder.Services.IncluirDependenciasDoProjeto();
builder.Services.ImplementarAutenticacao(builder.Configuration);

#endregion

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Testes de Integratte.Infra", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = @"JWT Authorization header - Bearer scheme. Digite 'Bearer + espaço + TokenJwt'.",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
});

var app = builder.Build();

#region Customizado - App

app.UseMiddleware(typeof(MiddlewareDeErros));

var chamadaHttp = app.Services.GetRequiredService<ChamadaHttp>();
TokenParaApiDeTestes.Criar(chamadaHttp);

#endregion

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
