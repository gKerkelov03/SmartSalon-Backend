using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Features.Salons.Commands;
using SmartSalon.Application.Features.Salons.Queries;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Presentation.Web.Controllers;
using SmartSalon.Presentation.Web.Salons.Requests;

namespace SmartSalon.Presentation.Web.Features.Salons.Controllers;

[Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
public class CurrenciesController(ISender _mediator, IMapper _mapper) : V1ApiController
{
    [HttpPost]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status409Conflict)]
    public async Task<IActionResult> AddCurrency(AddCurrencyRequest request)
    {
        var command = _mapper.Map<AddCurrencyCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpGet(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    public async Task<IActionResult> GetCurrencyById(Id currencyId)
    {
        var command = new GetCurrencyByIdQuery(currencyId);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result => Ok(result.Value), result);
    }

    [HttpDelete]
    [SuccessResponse(Status200OK)]
    public async Task<IActionResult> RemoveCurrency(RemoveCurrencyRequest request)
    {
        var command = _mapper.Map<RemoveCurrencyCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }
}
