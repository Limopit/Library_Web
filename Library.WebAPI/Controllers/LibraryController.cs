using System.Security.Claims;
using Library.Application.Authors.Commands.CreateAuthor;
using Library.Application.Authors.Commands.DeleteAuthor;
using Library.Application.Authors.Commands.UpdateAuthor;
using Library.Application.Authors.Queries.GetAuthorBooksList;
using Library.Application.Authors.Queries.GetAuthorById;
using Library.Application.Authors.Queries.GetAuthorList;
using Library.Application.Books.Commands.AddImage;
using Library.Application.Books.Commands.CreateBook;
using Library.Application.Books.Commands.DeleteBook;
using Library.Application.Books.Commands.UpdateBook;
using Library.Application.Books.Queries.GetBookById;
using Library.Application.Books.Queries.GetBookByISBN;
using Library.Application.Books.Queries.GetBooksList;
using Library.Application.BorrowRecords.Commands.CreateBorrowRecord;
using Library.Application.BorrowRecords.Queries.GetExpiringBorrowRecord;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers;

[Route("api/[controller]")]
public class LibraryController : BaseController
{
    public LibraryController(IMediator mediator) : base(mediator){}
    
    [HttpGet("author/")]
    public async Task<ActionResult<AuthorListVm>> GetAllAuthors(int pageNumber = 1, int pageSize = 10)
    {
        var authors = await _mediator.Send(new GetAuthorListQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        });
        return Ok(authors);
    }
    
    [HttpGet("author/books/{id}")]
    public async Task<ActionResult<AuthorListVm>> GetAllAuthorBooks(Guid id)
    {
        var books = await _mediator.Send(new GetAuthorBooksListQuery(){author_id = id});
        return Ok(books);
    }
    
    [HttpGet("author/{id}")]
    public async Task<ActionResult<AuthorDetailsVm>> GetAuthor(Guid id)
    {
        var author = await _mediator.Send(new GetAuthorByIdQuery() { author_id = id });
        return Ok(author);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("author/")]
    public async Task<ActionResult<Guid>> CreateNewAuthor([FromBody] CreateAuthorCommand command)
    {
        var author_id = await _mediator.Send(command);
        return Ok(author_id);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("author/")]
    public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("author/")]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        var command = new DeleteAuthorCommand()
        {
            author_id = id
        };
        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("books/")]
    public async Task<ActionResult<Guid>> AddBook([FromBody] CreateBookCommand command)
    {
        var book_id = await _mediator.Send(command);
        return Ok(book_id);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("books/")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        var command = new DeleteBookCommand()
        {
            book_id = id
        };
        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("books/")]
    public async Task<ActionResult> UpdateBook([FromBody] UpdateBookCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("books/")]
    public async Task<ActionResult<BooksListVm>> GetBooksList(int pageNumber = 1, int pageSize = 10)
    {
        var books = await _mediator.Send(new GetBooksListQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        });
        return Ok(books);
    }
    
    [HttpGet("books/id/{id}")]
    public async Task<ActionResult<BooksListVm>> GetBookById(Guid id)
    {
        var book = await _mediator.Send(new GetBookByIdQuery() {book_id = id});
        return Ok(book);
    }
    
    [HttpGet("books/ISBN/{ISBN}")]
    public async Task<ActionResult<BooksListVm>> GetBookById(string ISBN)
    {
        var book = await _mediator.Send(new GetBookByISBNQuery() {ISBN = ISBN});
        return Ok(book);
    }

    [Authorize]
    [HttpPost("books/issue/")]
    public async Task<ActionResult<Guid>> CreateBorrowRecord([FromBody] CreateBorrowRecordCommand command)
    {
        command.Email = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

    [Authorize]
    [HttpGet("user/borrows/expiring")]
    public async Task<ActionResult<ExpiringRecordVm>> GetExpiringRecord()
    {
        var command = new GetExpiringBorrowRecordQuery
        {
            Email = User.FindFirstValue(ClaimTypes.NameIdentifier)
        };
        
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("books/{bookId}/add-image")]
    public async Task<IActionResult> AddImageToBook([FromBody]AddImageCommand command)
    {
        var path = Path.Combine("wwwroot", "images","books", command.ImagePath);
        command.ImagePath = path;
        await _mediator.Send(command);
    
        return NoContent();
    }
}