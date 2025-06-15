﻿using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Application.Interfaces.Persistence.IRepositories;
using Fanitty.Server.Application.Mappers;
using Fanitty.Server.Application.Queries.Users;
using Fanitty.Server.Application.Responses.Users;
using MediatR;

namespace Fanitty.Server.Application.Handlers.Users;
public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, CurrentUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetCurrentUserQueryHandler(IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CurrentUserResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var user = await _userRepository.GetUserByIdAsync(userId, cancellationToken);
        return user.MapToCurrentUserResponse();
    }
}
