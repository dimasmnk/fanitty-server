namespace Fanitty.Server.Application.Interfaces;
public interface IUsernameGeneratorService
{
    string GenerateUsernameFromEmail(string email, int randomDigitCount, int maxLength);
}
