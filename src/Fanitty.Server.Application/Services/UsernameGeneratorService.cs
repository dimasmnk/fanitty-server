using Fanitty.Server.Application.Interfaces;
using System.Net.Mail;
using System.Text;

namespace Fanitty.Server.Application.Services;
public class UsernameGeneratorService : IUsernameGeneratorService
{
    private readonly Random _random;

    public UsernameGeneratorService()
    {
        _random = new Random();
    }

    public string GenerateUsernameFromEmail(string email, int randomDigitCount, int maxLength)
    {
        var displayName = new MailAddress(email).User;
        var digits = GetRandomDigits(randomDigitCount);
        var username = new StringBuilder();
        username.Append(displayName);
        username.Append(digits);
        username.Length = username.Length > maxLength
            ? maxLength
            : username.Length;
        return username.ToString();
    }

    private int GetRandomDigits(int randomDigitCount)
    {
        var startRange = Convert.ToInt32(Math.Pow(10, randomDigitCount - 1));
        var endRange = Convert.ToInt32(Math.Pow(10, randomDigitCount));
        return _random.Next(startRange, endRange);
    }
}
