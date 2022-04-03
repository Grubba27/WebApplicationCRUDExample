using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationCRUDExample.Models;
using WebApplicationCRUDExample.Services;

namespace WebApplicationCRUDExample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibraryController : Controller
{
    private readonly LibraryService _libraryService;

    public LibraryController(LibraryService libraryService)
    {
        _libraryService = libraryService;
    }


    [HttpGet("/books")]
    [Authorize]
    public async Task<List<Book>> GetBooks()
    {
        return await _libraryService.GetBookAsync();
    }

    [HttpGet("/books/{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Book>> GetBookById(string id)
    {
        var book = await _libraryService.GetBookByIdAsync(id);

        if (book is null) return NotFound();

        return book;
    }

    [HttpPost("/books/")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> PostBook(Book book)
    {
        await _libraryService.CreateBookAsync(book);
        return CreatedAtAction(nameof(GetBookById), new {id = book.Id}, book);
    }

    [HttpPut("/books/{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateBook(string id, Book updatedBook)
    {
        var oldBook = await _libraryService.GetBookByIdAsync(id);

        if (oldBook is null) return NotFound();

        await _libraryService.UpdateBookAsync(id, updatedBook);

        return NoContent();
    }

    [HttpDelete("/books/{id:length(24)}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteBook(string id)
    {
        var book = await _libraryService.GetBookByIdAsync(id);

        if (book is null) return NotFound();

        await _libraryService.RemoveBookAsync(id);

        return NoContent();
    }
}