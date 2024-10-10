using Library.Application.Authors.Commands.CreateAuthor;
using Library.Application.Authors.Commands.DeleteAuthor;
using Library.Application.Authors.Commands.UpdateAuthor;
using Library.Application.Authors.Queries.GetAuhtorList;
using Library.Application.Authors.Queries.GetAuthorBooksList;
using Library.Application.Authors.Queries.GetAuthorDetails;
using Library.Application.Books.Commands.CreateBook;
using Library.Application.Books.Commands.DeleteBook;
using Library.Application.Books.Commands.UpdateBook;
using Library.Application.Books.Queries;
using Library.Application.Books.Queries.GetBookById;
using Library.Application.Books.Queries.GetBookByISBN;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers;

[Route("api/[controller]")]
public class LibraryController : BaseController
{
    public LibraryController(IMediator mediator) : base(mediator){}
    
    [HttpGet("author/")]
    public async Task<ActionResult<AuthorListVm>> GetAllAuthors()
    {
        var authors = await _mediator.Send(new GetAuthorListQuery());
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

    [HttpPost("author/")]
    public async Task<ActionResult<Guid>> CreateNewAuthor([FromBody] CreateAuthorCommand command)
    {
        var author_id = await _mediator.Send(command);
        return Ok(author_id);
    }

    [HttpPut("author/")]
    public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

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

    [HttpPost("books/")]
    public async Task<ActionResult<Guid>> AddBook([FromBody] CreateBookCommand command)
    {
        var book_id = await _mediator.Send(command);
        return Ok(book_id);
    }

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

    [HttpPut("books/")]
    public async Task<ActionResult> UpdateBook([FromBody] UpdateBookCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("books/")]
    public async Task<ActionResult<BooksListVm>> GetBooksList()
    {
        var books = await _mediator.Send(new GetBooksListQuery());
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
}