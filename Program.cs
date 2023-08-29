using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//adiciona serviço dos controllers

builder.Services.AddControllers();

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

app.UseHttpsRedirection();

app.Run();
