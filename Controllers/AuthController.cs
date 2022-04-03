using Microsoft.AspNetCore.Mvc;
using WebApplicationCRUDExample.Services;

namespace WebApplicationCRUDExample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly UserService _userService;

    public AuthController(UserService userService)
    {
        _userService = userService;
    }


    [HttpPost("/auth/")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<dynamic>> Authenticate([FromBody] string id)
    {
        // Obtem esse id via email/hash e usa para logar o user;

        var user = await _userService.GetUserByIdAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        var token = AuthService.GenerateToken(user);

        return new
        {
            user = user,
            token = token
        };
    }
}