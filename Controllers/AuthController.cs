using Microsoft.AspNetCore.Mvc;
using WebApplicationCRUDExample.Models;
using WebApplicationCRUDExample.Services;

namespace WebApplicationCRUDExample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly LibraryService _libraryService;

    public AuthController(LibraryService libraryService)
    {
        _libraryService = libraryService;
    }


    [HttpPost("/auth/")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<dynamic>> Authenticate([FromBody] string id)
    {
        // Verifica antes se os hash das senhas batem;

        var user = await _libraryService.GetUserByIdAsync(id);

        // Verifica se o usuário existe
        if (user is null)
        {
            return NotFound();
        }

        // Gera o Token
        var token = AuthService.GenerateToken(user);

        return new
        {
            user = user,
            token = token
        };
    }
}