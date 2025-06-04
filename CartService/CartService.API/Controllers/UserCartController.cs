using CartService.Application.Commands;
using CartService.Application.DTOs;
using CartService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CartService.API.Controllers;

[ApiController]
[Route("carts")]
[Authorize]
public class UserCartController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<ActionResult<CartDto?>> Get()
        => Ok(await _mediator.Send(new GetCartQuery(GetUserId())));

    [HttpPost("items")]
    public async Task<IActionResult> AddItem([FromBody] CartItemDto item)
    {
        await _mediator.Send(new AddItemCommand(GetUserId(), item));
        return Ok();
    }

    [HttpDelete("items/{productId}")]
    public async Task<IActionResult> RemoveItem(Guid productId)
    {
        await _mediator.Send(new RemoveItemCommand(GetUserId(), productId));
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Clear()
    {
        await _mediator.Send(new ClearCartCommand(GetUserId()));
        return NoContent();
    }
}
