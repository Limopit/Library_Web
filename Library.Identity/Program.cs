using Library.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer(options =>
    {
        options.EmitStaticAudienceClaim = true; // Опция для добавления статического audience claim
    })
    .AddInMemoryClients(IdentityServerConfig.GetClients())           // Конфигурация клиентов
    .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())       // Конфигурация API Scopes
    .AddDeveloperSigningCredential();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIdentityServer();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();