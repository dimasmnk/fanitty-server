using AutoFixture.Xunit2;
using Fanitty.Server.Application.Handlers.Users;
using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Application.Interfaces.Persistence.IRepositories;
using Fanitty.Server.Application.Queries.Users;
using Fanitty.Server.Application.Responses.Users;
using Fanitty.Server.Core.Entities;
using FluentAssertions;
using NSubstitute;

namespace Fanitty.Server.Application.UnitTests.Handlers.Users;
public class GetCurrentUserQueryHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly GetCurrentUserQueryHandler _getCurrentUserQueryHandler;

    public GetCurrentUserQueryHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _currentUserService = Substitute.For<ICurrentUserService>();
        _getCurrentUserQueryHandler = new GetCurrentUserQueryHandler(_userRepository, _currentUserService);
    }

    [Theory]
    [AutoData]
    public async Task Handle_UserExists_ShouldReturnUser(Guid userId)
    {
        // Arrange
        var expectedResponse = new CurrentUserResponse
        {
            Id = userId
        };

        var user = new User { Id = userId };

        _currentUserService.GetUserId().Returns(userId);
        _userRepository.GetUserByIdAsync(userId, CancellationToken.None).Returns(user);

        // Act
        var actualResponse = await _getCurrentUserQueryHandler.Handle(new GetCurrentUserQuery(), CancellationToken.None);

        // Assert
        actualResponse.Should().BeEquivalentTo(expectedResponse);
    }
}
