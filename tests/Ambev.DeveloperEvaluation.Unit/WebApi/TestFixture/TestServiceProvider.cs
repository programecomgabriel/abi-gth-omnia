using Ambev.DeveloperEvaluation.Application.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.Unit.WebApi.TestFixture;

public static class TestServiceProvider
{
    public static ServiceProvider Build()
    {
        var services = new ServiceCollection();

        var applicationAssembly = typeof(CreateUserCommand).Assembly;

        // MediatR
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(applicationAssembly));

        // FluentValidation
        services.AddValidatorsFromAssembly(applicationAssembly);

        // Pipeline
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services.BuildServiceProvider();
    }
}
