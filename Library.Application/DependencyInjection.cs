using System.Reflection;
using FluentValidation;
using Library.Application.Authors.Commands.CreateAuthor;
using Library.Application.Common.Behavior;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg
            =>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        services.AddValidatorsFromAssemblyContaining<CreateAuthorCommand>();
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
        
        return services;
    }
}