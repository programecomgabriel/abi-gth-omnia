using Ambev.DeveloperEvaluation.Application.Features.Carts.CancelItemCart;
using Ambev.DeveloperEvaluation.Application.Features.Carts.ChangeQuantityItemCart;
using Ambev.DeveloperEvaluation.Application.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CancelItemCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.ChangeQuantityItemCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

/// <summary>
/// Controller for managing Cart operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CartsController(IMediator mediator) : BaseController
{
    /// <summary>
    /// Creates a new cart
    /// </summary>
    /// <param name="command">The cart creation command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCartResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCart([FromServices] Session session, [FromBody] CreateCartRequest request, CancellationToken cancellationToken)
    {
        var sessionUser = session.GetUser();

        if (!Guid.TryParse(sessionUser.Id, out var userId))
        {
            throw new InvalidOperationException("Invalid user ID in session.");
        }

        var command = new CreateCartCommand
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            Branch = request.Branch,
            UserId = userId
        };

        var response = await mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateCartResult>
        {
            Success = true,
            Message = "Cart created successfully",
            Data = response
        });
    }

    /// <summary>
    /// Change quantity of an item in the cart
    /// </summary>
    /// <param name="command">The change quantity of an item in the cart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<ChangeQuantityItemCartResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeQuantityItemCart([FromServices] Session session, [FromBody] ChangeQuantityItemCartRequest request, CancellationToken cancellationToken)
    {
        var sessionUser = session.GetUser();

        if (!Guid.TryParse(sessionUser.Id, out var userId))
        {
            throw new InvalidOperationException("Invalid user ID in session.");
        }

        var command = new ChangeQuantityItemCartCommand
        {
            Id = request.Id,
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            UserId = userId
        };

        var response = await mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<ChangeQuantityItemCartResult>
        {
            Success = true,
            Message = "Item quantity changed successfully",
            Data = response
        });
    }

    /// <summary>
    /// Creates a new cart
    /// </summary>
    /// <param name="command">The cancel item in the cart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CancelItemCartResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelItemCart([FromServices] Session session, [FromBody] CancelItemCartRequest request, CancellationToken cancellationToken)
    {
        var sessionUser = session.GetUser();

        if (!Guid.TryParse(sessionUser.Id, out var userId))
        {
            throw new InvalidOperationException("Invalid user ID in session.");
        }
        var command = new CancelItemCartCommand
        {
            Id = request.Id,
            ProductId = request.ProductId,
            UserId = userId
        };
        var response = await mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CancelItemCartResult>
        {
            Success = true,
            Message = "Item canceled successfully",
            Data = response
        });
    }
}
