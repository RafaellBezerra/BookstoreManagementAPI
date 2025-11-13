using BookstoreManagementApi.Communications.Requests;
using BookstoreManagementApi.Communications.Responses;
using BookstoreManagementApi.Exceptions;
using BookstoreManagementApi.UseCases.Create;
using BookstoreManagementApi.UseCases.Delete;
using BookstoreManagementApi.UseCases.GetAll;
using BookstoreManagementApi.UseCases.GetById;
using BookstoreManagementApi.UseCases.Update;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreManagementApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookstoreController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseCreateBook), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(
        [FromBody] RequestBook request,
        [FromServices] CreateBookUseCase useCase)
    {
        try
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }
        catch (ErrorOnValidationException ex)
        {
            return BadRequest(ex._errorMessages);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseGetAllBooks), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromQuery] RequestFilterForGetAll requestFilter, [FromServices] GetAllBooksUseCase useCase)
    {
        var response = await useCase.Execute(requestFilter);

        if (response.Count > 0)
            return Ok(response);

        return NoContent();
    }

    [HttpGet]
    [Route("{Id}")]
    [ProducesResponseType(typeof(ResponseGetBookById), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid Id, [FromServices] GetBookByIdUsecase usecase)
    {
        try
        {
            var response = await usecase.Execute(Id);
            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid Id, [FromServices] DeleteBookUseCase useCase)
    {
        try
        {
            await useCase.Execute(Id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut]
    [Route("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid Id,
        [FromBody] RequestBook request,
        [FromServices] UpdateBookUseCase useCase)
    {
        try
        {
            await useCase.Execute(request, Id);
            return NoContent();
        }
        catch (ErrorOnValidationException ex)
        {
            return BadRequest(ex._errorMessages);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
}
