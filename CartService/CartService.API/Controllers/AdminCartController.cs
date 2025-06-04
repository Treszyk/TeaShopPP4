using CartService.Application.Commands;
using CartService.Application.DTOs;
using CartService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartService.API.Controllers;

[ApiController]
[Route("admin")]
[Authorize(Roles = "Admin")]
public class AdminCartController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("carts")]
    public async Task<ActionResult<List<CartDto>>> GetAll()
        => Ok(await _mediator.Send(new GetAllCartsQuery()));

    [HttpGet("carts/{userId}")]
    public async Task<ActionResult<CartDto?>> Get(Guid userId)
        => Ok(await _mediator.Send(new GetCartQuery(userId)));

    [HttpPost("carts/{userId}/items")]
    public async Task<IActionResult> AddItem(Guid userId, [FromBody] CartItemDto item)
    {
        await _mediator.Send(new AddItemCommand(userId, item));
        return Ok();
    }

    [HttpDelete("carts/{userId}/items/{productId}")]
    public async Task<IActionResult> RemoveItem(Guid userId, Guid productId)
    {
        await _mediator.Send(new RemoveItemCommand(userId, productId));
        return NoContent();
    }

    [HttpDelete("carts/{userId}")]
    public async Task<IActionResult> Clear(Guid userId)
    {
        await _mediator.Send(new ClearCartCommand(userId));
        return NoContent();
    }
}
