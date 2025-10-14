using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NetCore_Mediator_CleanArch.API.Models;
using NetCore_Mediator_CleanArch.Domain.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetCore_Mediator_CleanArch.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController(IAuthenticate authentication, IConfiguration configuration) : ControllerBase
{
    private readonly IAuthenticate _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));

    [Authorize]
    [HttpPost("CreateUser")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult> CreateUser([FromBody] LoginModel userInfo)
    {
        bool result = await _authentication.RegisterUser(userInfo.Email!, userInfo.Password!);

        if (result)
            return Ok($"User {userInfo.Email} was create successfully");
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
            return BadRequest(ModelState);
        }
    }

    [AllowAnonymous]
    [HttpPost("LoginUser")]
    public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
    {
        bool result = await _authentication.Authenticate(userInfo.Email!, userInfo.Password!);

        if (result)
            return GenerateToken(userInfo);
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
            return BadRequest(ModelState);
        }
    }

    private UserToken GenerateToken(LoginModel userInfo)
    {
        // declarações do usuário
        Claim[] claims =
        [
            new Claim("email", userInfo.Email!),
            new Claim("valorteste", "teste valor"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        // gerar chave privada para gerar o token
        SymmetricSecurityKey privateKey = new(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!));

        // gerar assinatura digital
        SigningCredentials credentials = new(privateKey, SecurityAlgorithms.HmacSha256);

        // definir o tempo de expiração
        DateTime expiration = DateTime.UtcNow.AddMinutes(10);

        // gerar o Token
        JwtSecurityToken token = new(
            issuer: configuration["Jwt:Issuer"], // emissor
            audience: configuration["Jwt:Audience"], // audiencia
            claims: claims, // claims
            expires: expiration, // data de expiração
            signingCredentials: credentials // assinatura digital
        );

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}
