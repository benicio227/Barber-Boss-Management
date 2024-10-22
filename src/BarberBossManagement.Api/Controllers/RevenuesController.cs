using BarberBossManagement.Application.UseCases.Revenues.Delete;
using BarberBossManagement.Application.UseCases.Revenues.GetAll;
using BarberBossManagement.Application.UseCases.Revenues.GetById;
using BarberBossManagement.Application.UseCases.Revenues.Register;
using BarberBossManagement.Application.UseCases.Revenues.Update;
using BarberBossManagement.Communication.Requests;
using BarberBossManagement.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberBossManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RevenuesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredRevenueJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterRevenueUseCase useCase,
        [FromBody] RequestRevenueJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseRevenuesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<IActionResult> GetAllRevenues([FromServices] IGetAllRevenueUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Revenues.Count != 0)
            return Ok(response);
        return NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseRevenuesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetById(
        [FromServices] IGetRevenueByIdUseCase useCase,
        [FromRoute] long id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(
        [FromServices] IDeleteRevenueUseCase useCase,
        [FromRoute] long id)
    {
        await useCase.Execute(id);

        return NoContent();
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Update(
        [FromServices] IUpdateRevenueUseCase useCase,
        [FromRoute] long id,
        [FromBody] RequestRevenueJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }
}
