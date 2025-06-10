using APBD_Test2.DTOs;
using APBD_Test2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Test2.Controllers;

[Route("api/books")]
[ApiController]
public class BookController(IBookService bookService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> AddNewBook(
        [FromQuery] AddBookDTO addBookDto
    )
    {
        try
        {
            await bookService.AddNewBookAsync(addBookDto);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}