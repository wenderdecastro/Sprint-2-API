using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

//adiciona serviço dos controllers

builder.Services.AddControllers();

// Aciona o serviço de JWT Bearer ( forma de autenticação )

builder.Services.AddAuthentication(options =>
    {
        options.DefaultChallengeScheme = "JwtBearer";
        options.DefaultAuthenticateScheme = "JwtBearer";
    })

.AddJwtBearer("JwtBearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Valida quem está solicitando
            ValidateIssuer = true,

            // Valida quem está recebendo
            ValidateActor = true,

            // Define se o tempo de expiração sera validado
            ValidateLifetime = true,

            // Forma de criptografia e valida a chave de autenticação
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("key-filmes.webapi.auth.dev-senai")),

            // Valida o tempo de expiração do token
            ClockSkew = TimeSpan.FromMinutes(5),

            // Nome do issuer (de onde está vindo)
            ValidIssuer = "webapi.Filmes",

            // Nome da audience (para onde está indo)
            ValidAudience = "webapi.Filmes"
        };
    });







builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API SENAI",
        Description = "Uma web API do ASP.NET Core para um CRUD de um aplicativo ficticio relacionado a filmes. ",
        Contact = new OpenApiContact
        {
            Name = "GitHub",
            Url = new Uri("https://github.com/wenderdecastro")

        },

    });

    // configura o swagger para utilizar o arquivo xml gerado

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    //Usando a autenticaçao no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT ",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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


// inicia a configuração do swagger

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

// termina a configuração do swagger



//adiciona mapeamento dos controllers
app.MapControllers();

// Adiciona autenticação
app.UseAuthentication();

// Adiciona autorização
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();
