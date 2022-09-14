namespace NetCore6_Mediator_CleanArch.API.Models;

public class UserToken
{
    public string? Token { get; set; }
    public DateTime Expiration { get; set; }
}
