using Library.Application.Authors.Commands.CreateAuthor;
using Library.Application.Authors.Commands.DeleteAuthor;
using Library.Application.Authors.Commands.UpdateAuthor;
using Library.Application.Authors.Queries.GetAuhtorList;
using Library.Application.Authors.Queries.GetAuthorDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers;

[Route("api/[controller]")]
public class LibraryController : BaseController
{
    public LibraryController(IMediator mediator) : base(mediator){}
    
    [HttpGet]
    public async Task<ActionResult<AuthorListVm>> GetAllAuthors()
    {
        var authors = await _mediator.Send(new GetAuthorListQuery());
        return Ok(authors);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDetailsVm>> GetAuthor(Guid id)
    {
        var author = await _mediator.Send(new GetAuthorDetailsQuery() { author_id = id });
        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateNewAuthor([FromBody] CreateAuthorCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.author_firstname) || string.IsNullOrWhiteSpace(command.author_lastname))
        {
            return BadRequest("Wrong author name");
        }

        var author_id = await _mediator.Send(command);
        return Ok(author_id);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.author_firstname) || string.IsNullOrWhiteSpace(command.author_lastname))
        {
            return BadRequest("Wrong author name");
        }

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        var command = new DeleteAuthorCommand()
        {
            author_id = id
        };
        await _mediator.Send(command);
        return NoContent();
    }
}