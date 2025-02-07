using Microsoft.AspNetCore.Mvc;

namespace BasicAPI.Models.Models;

public class AuthParameters
{
    [FromHeader(Name ="UserName")]
    public required string UserName { get; set; }
    [FromHeader(Name = "Password")]
    public required string Password { get; set; }

    [FromHeader(Name = "Database")]
    public required string Database { get; set; }
}
