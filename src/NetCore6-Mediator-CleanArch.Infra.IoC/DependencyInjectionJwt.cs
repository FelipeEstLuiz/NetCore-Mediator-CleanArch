using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NetCore6_Mediator_CleanArch.Infra.IoC
{
    public static class DependencyInjectionJwt
    {
        public static IServiceCollection AddInfrastructureJwt(
            this IServiceCollection services,
            string secretKey,
            string issuer,
            string audience
        )
        {
            // informar o tipo de autenticação JWT-Bearer
            // definir o modelo de desafio de autenticação
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // habilita a autenticação JWT usando o esquema e desafio definidos
            // validar token
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    // valor validos
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
