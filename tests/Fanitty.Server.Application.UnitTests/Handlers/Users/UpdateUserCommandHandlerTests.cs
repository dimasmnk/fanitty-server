using AutoFixture.Xunit2;
using Fanitty.Server.Application.Commands.Users;
using Fanitty.Server.Application.Handlers.Users;
using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Application.Interfaces.Persistence;
using Fanitty.Server.Application.Interfaces.Persistence.IRepositories;
using Fanitty.Server.Core.Entities;
using FluentAssertions;
using NSubstitute;

namespace Fanitty.Server.Application.UnitTests.Handlers.Users;
public class UpdateUserCommandHandlerTests
{
    private readonly IFanittyDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly UpdateUserCommandHandler _updateUserCommandHandler;

    public UpdateUserCommandHandlerTests()
    {
        _dbContext = Substitute.For<IFanittyDbContext>();
        _userRepository = Substitute.For<IUserRepository>();
        _currentUserService = Substitute.For<ICurrentUserService>();
        _updateUserCommandHandler = new UpdateUserCommandHandler(_dbContext, _userRepository, _currentUserService);
    }

    [Theory]
    [AutoData]
    public async Task Handle_AllPropertiesAreSet_ShouldUpdateAll(UpdateUserCommand request, User user)
    {
        // Arrange
        var expectedUser = new User
        {
            Id = user.Id,
            Uid = user.Uid,
            Email = user.Email,
            Username = request.Username!,
            DisplayName = request.DisplayName!,
            Bio = request.Bio
        };

        _currentUserService.GetUserId().Returns(user.Id);
        _userRepository.GetUserByIdAsync(user.Id, CancellationToken.None).Returns(user);
        _userRepository.IsUsernameAvailableAsync(request.Username!, CancellationToken.None).Returns(true);

        // Act
        await _updateUserCommandHandler.Handle(request, CancellationToken.None);

        // Assert
        user.Should().BeEquivalentTo(expectedUser);
    }
}
