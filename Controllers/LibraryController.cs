using Microsoft.AspNetCore.Mvc;
using WebApplicationCRUDExample.Models;
using WebApplicationCRUDExample.Services;

namespace WebApplicationCRUDExample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibraryController : ControllerBase
{
    private readonly LibraryService _libraryService;

    public LibraryController(LibraryService libraryService) =>
        _libraryService = libraryService;

    #region Books

    [HttpGet("/books")]
    public async Task<List<Book>> GetBooks() => await _libraryService.GetBookAsync();

    [HttpGet("/books/{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Book>> GetBookById(string id)
    {
        var book = await _libraryService.GetBookByIdAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost("/books/")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> PostBook(Book book)
    {
        await _libraryService.CreateBookAsync(book);
        return CreatedAtAction(nameof(GetBookById), new {id = book.Id}, book);
    }

    [HttpPut("/books/{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateBook(string id, Book updatedBook)
    {
        var oldBook = await _libraryService.GetBookByIdAsync(id);

        if (oldBook is null)
        {
            return NotFound();
        }

        await _libraryService.UpdateBookAsync(id, updatedBook);

        return NoContent();
    }

    [HttpDelete("/books/{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteBook(string id)
    {
        var book = await _libraryService.GetBookByIdAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _libraryService.RemoveBookAsync(id);

        return NoContent();
    }

    #endregion


    #region Users

    [HttpGet("/users/")]
    public async Task<List<User>> GetUsers() => await _libraryService.GetUserAsync();

    [HttpGet("/users/{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<User>> GetUserById(string id)
    {
        var user = await _libraryService.GetUserByIdAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpGet("/users/{id:length(24)}/likes")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Book>>> GetUserLikes(string id)
    {
        var user = await _libraryService.GetUserByIdAsync(id);
        List<Book> bookList = new List<Book>();

        if (user is null)
        {
            return NotFound();
        }
        
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
        await _libraryService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new {id = user.Id}, user);
    }

    [HttpPut("/users/{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateUser(string id, User updatedUser)
    {
        var oldUser = await _libraryService.GetUserByIdAsync(id);

        if (oldUser is null)
        {
            return NotFound();
        }

        await _libraryService.UpdateUserAsync(id, updatedUser);
        return NoContent();
    }


    [HttpDelete("/users/{id:length(24)}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _libraryService.GetUserByIdAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        await _libraryService.RemoveUserAsync(id);

        return NoContent();
    }

    #endregion
}