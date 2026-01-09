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
    public async Task<IActionResult> CreateCart([FromBody] CreateCartCommand command, CancellationToken cancellationToken)
    {
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
    public async Task<IActionResult> ChangeQuantityItemCart([FromBody] ChangeQuantityItemCartCommand command, CancellationToken cancellationToken)
    {
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
    public async Task<IActionResult> CancelItemCart([FromBody] CancelItemCartCommand command, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CancelItemCartResult>
        {
            Success = true,
            Message = "Item canceled successfully",
            Data = response
        });
    }
}
