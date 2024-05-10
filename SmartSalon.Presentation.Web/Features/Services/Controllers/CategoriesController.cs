using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCategory.Application.Features.Services.Commands;
using SmartSalon.Application.Features.Services.Commands;
using SmartSalon.Presentation.Web.Attributes;
using SmartSalon.Presentation.Web.Controllers;
using SmartSalon.Presentation.Web.Features.Services.Requests;

namespace SmartSalon.Presentation.Web.Features.Services.Controllers;

public class CategoriesController(ISender _mediator, IMapper _mapper) : V1ApiController
{
    [HttpPost]
    [SuccessResponse(Status200OK)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
    {
        var command = _mapper.Map<CreateCategoryCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr(result =>
            Created(result.Value.CreatedCategoryId),
            result
        );
    }

    [HttpPatch(IdRoute)]
    [SuccessResponse(Status200OK)]
    [FailureResponse(Status404NotFound)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryRequest request)
    {
        var command = _mapper.Map<UpdateCategoryCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<OkResult>(result);
    }

    [HttpDelete]
    [SuccessResponse(Status200OK)]
    [Authorize(Policy = IsOwnerOfTheSalonOrIsAdminPolicy)]
    public async Task<IActionResult> DeleteCategory(DeleteCategoryRequest request)
    {
        var command = _mapper.Map<DeleteCategoryCommand>(request);
        var result = await _mediator.Send(command);

        return ProblemDetailsOr<NoContentResult>(result);
    }
}
