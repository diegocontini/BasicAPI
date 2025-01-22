using Microsoft.AspNetCore.Mvc;

namespace BasicAPI.Models.Models;

public class AuthParameters
{
    [FromHeader]
    public required string UserName { get; set; }
    [FromHeader]
    public required string Password { get; set; }
}
