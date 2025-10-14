using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore_Mediator_CleanArch.Application.Interfaces;
using NetCore_Mediator_CleanArch.Application.Services;
using NetCore_Mediator_CleanArch.Domain.Account;
using NetCore_Mediator_CleanArch.Domain.Interfaces;
using NetCore_Mediator_CleanArch.Infra.Data.Context;
using NetCore_Mediator_CleanArch.Infra.Data.Identity;
using NetCore_Mediator_CleanArch.Infra.Data.Repositories;

namespace NetCore_Mediator_CleanArch.Infra.IoC;

public static class DependencyInjectionApi
{
    public static IServiceCollection AddInfrastructureApi(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(
                connectionString,
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            )
        );

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IAuthenticate, AuthenticateService>();

        var myHandlers = AppDomain.CurrentDomain.Load("NetCore-Mediator-CleanArch.Application");
        services.AddMediatR(myHandlers);

        return services;
    }
}
