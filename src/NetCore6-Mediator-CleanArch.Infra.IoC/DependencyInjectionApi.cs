using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore6_Mediator_CleanArch.Application.Interfaces;
using NetCore6_Mediator_CleanArch.Application.Mappings;
using NetCore6_Mediator_CleanArch.Application.Services;
using NetCore6_Mediator_CleanArch.Domain.Account;
using NetCore6_Mediator_CleanArch.Domain.Interfaces;
using NetCore6_Mediator_CleanArch.Infra.Data.Context;
using NetCore6_Mediator_CleanArch.Infra.Data.Identity;
using NetCore6_Mediator_CleanArch.Infra.Data.Repositories;

namespace NetCore6_Mediator_CleanArch.Infra.IoC
{
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

            services.AddAutoMapper(typeof(DomainToDtoMappingProfile));
            services.AddAutoMapper(typeof(DtoToCommandMappingProfile));

            var myHandlers = AppDomain.CurrentDomain.Load("NetCore6_Mediator_CleanArch.Application");
            services.AddMediatR(myHandlers);

            return services;
        }
    }
}
