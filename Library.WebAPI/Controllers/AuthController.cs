using MediatR;

namespace Library.WebAPI.Controllers;

public class AuthController: BaseController
{
    public AuthController(IMediator mediator) : base(mediator)
    {
    }
}