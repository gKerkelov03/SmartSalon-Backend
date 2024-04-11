﻿using AutoMapper;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Queries;

public class GetOwnerByIdQuery(Id ownerId) : IQuery<GetOwnerByIdQueryResponse>
{
    public Id OwnerId => ownerId;
}

public class GetOwnerByIdQueryResponse
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string PictureUrl { get; set; }
}

internal class GetOwnerByIdQueryHandler(IEfRepository<Owner> _workers, IMapper _mapper)
    : IQueryHandler<GetOwnerByIdQuery, GetOwnerByIdQueryResponse>
{
    public async Task<Result<GetOwnerByIdQueryResponse>> Handle(GetOwnerByIdQuery query, CancellationToken cancellationToken)
    {
        var worker = await _workers.GetByIdAsync(query.OwnerId);

        if (worker is null)
        {
            return Error.NotFound;
        }

        return _mapper.Map<GetOwnerByIdQueryResponse>(worker);
    }
}
