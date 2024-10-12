using Library.Application.Interfaces;
using Library.Application.Users.Commands.LoginUser;
using Library.Identity;
using Library.Identity.Configuration;
using Library.Persistance.Services;
using MediatR;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer(options =>
    {
        options.EmitStaticAudienceClaim = true;
    })
    .AddInMemoryClients(IdentityServerConfig.GetClients())
    .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
    .AddDeveloperSigningCredential();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIdentityServer();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();