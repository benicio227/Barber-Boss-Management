using BarberBossManagement.Application.UseCases.Barbers.Delete;
using BarberBossManagement.Application.UseCases.Barbers.GetAll;
using BarberBossManagement.Application.UseCases.Barbers.GetById;
using BarberBossManagement.Application.UseCases.Barbers.Register;
using BarberBossManagement.Application.UseCases.Barbers.Update;
using BarberBossManagement.Communication.Requests;
using BarberBossManagement.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BarberBossManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BarberShopController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseBarberShopJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Register(
        [FromServices] IRegisterBarberShopUseCase useCase,
        [FromBody] RequestBarberShopJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseBarbersShopJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<IActionResult> GetAllBarbers([FromServices] IGetAllBarberUseCase useCase)
    {
        var response = await useCase.Execute();

        if(response.Barbers.Count != 0)
            return Ok(response);
        return NoContent();

    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseBarbersShopJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetById(
        [FromServices] IGetBarberByIdUseCase useCase,
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
        [FromServices] IDeleteBarberUseCase useCase,
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
        [FromServices] IUpdateBarberUseCase useCase,
        [FromRoute] long id,
        [FromBody] RequestBarberShopJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }
}
