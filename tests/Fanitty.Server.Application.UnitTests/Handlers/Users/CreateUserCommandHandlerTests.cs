using Fanitty.Server.Application.Commands.Users;
using Fanitty.Server.Application.Handlers.Users;
using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Application.Interfaces.Persistence;
using Fanitty.Server.Application.Interfaces.Persistence.IRepositories;
using Fanitty.Server.Core.Entities;
using Fanitty.Server.Core.Settings;
using NSubstitute;

namespace Fanitty.Server.Application.UnitTests.Handlers.Users;

public class CreateUserCommandHandlerTests
{
    private readonly IFanittyDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IFirebaseService _firebaseService;
    private readonly IUsernameGeneratorService _usernameGeneratorService;
    private readonly CreateUserCommandHandler _createUserCommandHandler;

    private const string _uid = "uid";
    private const string _email = "email@email.com";
    private const string _username = "username";
    private const string _generatedUsername = "generatedUsername1234";

    public CreateUserCommandHandlerTests()
    {
        _context = Substitute.For<IFanittyDbContext>();
        _userRepository = Substitute.For<IUserRepository>();
        _currentUserService = Substitute.For<ICurrentUserService>();
        _firebaseService = Substitute.For<IFirebaseService>();
        _usernameGeneratorService = Substitute.For<IUsernameGeneratorService>();
        _createUserCommandHandler = new CreateUserCommandHandler(
            _context,
            _userRepository,
            _currentUserService,
            _firebaseService,
            _usernameGeneratorService);
    }

    [Fact]
    public async Task Handle_GeneratedUsernameTrue_ShouldCreateUserWithGeneratedUsername()
    {
        // Arrange
        var request = new CreateUserCommand
        {
            IsGeneratedUsername = true,
            Username = _username,
        };

        _currentUserService.GetUid().Returns(_uid);
        _firebaseService.GetUserEmailByUidAsync(_uid).Returns(_email);
        _usernameGeneratorService.GenerateUsernameFromEmail(_email, 4, UserSettings.UsernameMaxLength).Returns(_generatedUsername);

        // Act
        await _createUserCommandHandler.Handle(request, CancellationToken.None);

        // Assert
        _userRepository.Received(1).Add(Arg.Is<User>(x => x.Username == _generatedUsername));
    }

    [Fact]
    public async Task Handle_GeneratedUsernameFalse_ShouldCreateUserWithSpecifiedUsername()
    {
        // Arrange
        var request = new CreateUserCommand
        {
            IsGeneratedUsername = false,
            Username = _username,
        };

        _currentUserService.GetUid().Returns(_uid);
        _firebaseService.GetUserEmailByUidAsync(_uid).Returns(_email);
        _usernameGeneratorService.GenerateUsernameFromEmail(_email, 4, UserSettings.UsernameMaxLength).Returns(_generatedUsername);

        // Act
        await _createUserCommandHandler.Handle(request, CancellationToken.None);

        // Assert
        _userRepository.Received(1).Add(Arg.Is<User>(x => x.Username == _username));
    }
}
