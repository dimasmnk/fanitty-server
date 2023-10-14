using AutoFixture.Xunit2;
using Fanitty.Server.Application.Handlers.Users;
using Fanitty.Server.Application.Interfaces.Persistence.IRepositories;
using Fanitty.Server.Application.Queries.Users;
using Fanitty.Server.Application.Responses.Users;
using Fanitty.Server.Core.Entities;
using FluentAssertions;
using NSubstitute;

namespace Fanitty.Server.Application.UnitTests.Handlers.Users;
public class GetUserByUsernameQueryHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly GetUserByUsernameQueryHandler _getUserByUsernameQueryHandler;

    public GetUserByUsernameQueryHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _getUserByUsernameQueryHandler = new GetUserByUsernameQueryHandler(_userRepository);
    }

    [Theory]
    [AutoData]
    public async Task Handle_UserExists_ShouldReturnUser(User user)
    {
        // Arrange
        var request = new GetUserByUsernameQuery(user.Username);
        var expectedResult = new UserByUsernameResponse
        {
            Username = user.Username,
            Id = user.Id,
            Bio = user.Bio,
            DisplayName = user.DisplayName
        };
        _userRepository.GetUserByUsernameAsync(user.Username, CancellationToken.None).Returns(user);

        // Act
        var actualResult = await _getUserByUsernameQueryHandler.Handle(request, CancellationToken.None);

        // Assert
        actualResult.Should().BeEquivalentTo(expectedResult);
    }
}
