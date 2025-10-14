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

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
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

        services.ConfigureApplicationCookie(options => options.AccessDeniedPath = "/Account/Login");

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IAuthenticate, AuthenticateService>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

        var myHandlers = AppDomain.CurrentDomain.Load("NetCore_Mediator_CleanArch.Application");
        services.AddMediatR(myHandlers);

        return services;
    }
}
