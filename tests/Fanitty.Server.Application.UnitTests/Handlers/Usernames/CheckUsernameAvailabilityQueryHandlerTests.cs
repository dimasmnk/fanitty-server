using Fanitty.Server.Application.Handlers.Usernames;
using Fanitty.Server.Application.Interfaces.Persistence.IRepositories;
using Fanitty.Server.Application.Queries.Usernames;
using NSubstitute;

namespace Fanitty.Server.Application.UnitTests.Handlers.Usernames;

public class CheckUsernameAvailabilityQueryHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly CheckUsernameAvailabilityQueryHandler _checkUsernameAvailabilityQueryHandler;

    public CheckUsernameAvailabilityQueryHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _checkUsernameAvailabilityQueryHandler = new CheckUsernameAvailabilityQueryHandler(_userRepository);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Handle_UsernameInputAvialability_ShouldReturnAppropriateStatus(bool isUsernameAvailable)
    {
        // Arrange
        const string username = "username";
        var request = new CheckUsernameAvailabilityQuery(username);
        _userRepository.IsUsernameAvailableAsync(username, CancellationToken.None).Returns(isUsernameAvailable);

        // Act
        var result = await _checkUsernameAvailabilityQueryHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(isUsernameAvailable, result.IsAvailable);
    }

}
