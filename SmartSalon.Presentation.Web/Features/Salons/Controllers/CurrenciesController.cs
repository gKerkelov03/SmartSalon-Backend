using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Features.Salons.Commands;
using SmartSalon.Application.Features.Salons.Queries;
using SmartSalon.Application.Features.Users.Queries;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Presentation.Web.Controllers;
using SmartSalon.Presentation.Web.Features.Salons.Responses;
using SmartSalon.Presentation.Web.Salons.Requests;
using SmartSalon.Application.Extensions;

namespace SmartSalon.Presentation.Web.Features.Salons.Controllers;

public class CurrenciesController(ISender _mediator, IMapper _mapper) : V1ApiController
{
    [HttpPost]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status409Conflict)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
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

    [HttpGet("Search")]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    [Authorize(Policy = IsOwnerOrIsAdminPolicy)]
    public async Task<IActionResult> SearchForCurrency(string searchTerm)
    {
        var query = new SearchForCurrencyQuery(searchTerm);
        var result = await _mediator.Send(query);

        return ProblemDetailsOr(result =>
            Ok(result.Value.ToListOf<GetCurrencyByIdResponse>(_mapper)),
            result
        );
    }

    [HttpDelete(IdRoute)]
    [SuccessResponse(Status200OK)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> RemoveCurrency(RemoveCurrencyRequest request)
    {
        var command = _mapper.Map<RemoveCurrencyCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }
}
