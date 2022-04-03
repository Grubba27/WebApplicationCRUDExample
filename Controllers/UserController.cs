using Microsoft.AspNetCore.Mvc;
using WebApplicationCRUDExample.Models;
using WebApplicationCRUDExample.Services;

namespace WebApplicationCRUDExample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly UserService _userService;
    private readonly LibraryService _libraryService;

    public UserController(UserService userService, LibraryService libraryService)
    {
        _userService = userService;
        _libraryService = libraryService;
    }


    [HttpGet("/users/")]
    public async Task<List<User>> GetUsers() => await _userService.GetUserAsync();

    [HttpGet("/users/{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<User>> GetUserById(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user is null) return NotFound();

        return user;
    }

    [HttpGet("/users/{id:length(24)}/likes")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Book>>> GetUserLikes(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        List<Book> bookList = new List<Book>();

        if (user is null) return NotFound();
        
        if (user.UserLikes is null) return BadRequest();

        foreach (var bookId in user.UserLikes)
        {
            var book = await _libraryService.GetBookByIdAsync(bookId);
            if (book is not null)
            {
                bookList.Add(book);
            }
        }

        return bookList;
    }

    [HttpPost("/users/")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> PostUser(User user)
    {
        await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new {id = user.Id}, user);
    }

    [HttpPut("/users/{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateUser(string id, User updatedUser)
    {
        var oldUser = await _userService.GetUserByIdAsync(id);

        if (oldUser is null) return NotFound();

        await _userService.UpdateUserAsync(id, updatedUser);
        return NoContent();
    }


    [HttpDelete("/users/{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user is null) return NotFound();

        await _userService.RemoveUserAsync(id);

        return NoContent();
    }
}