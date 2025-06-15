using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Application.Services.UsernameGenerator.Data;
using System.Text;

namespace Fanitty.Server.Application.Services.UsernameGenerator;
public class UsernameGeneratorService : IUsernameGeneratorService
{
    private readonly Random _random;
    private readonly string[] _adjectives;
    private readonly string[] _names;
    private const int _randomDigitCount = 3;

    public UsernameGeneratorService()
    {
        _random = new Random();
        _adjectives = Adjectives.Values;
        _names = Names.Values;
    }

    public string GenerateUsername()
    {
        var username = new StringBuilder();
        username.Append(GetRandomAdjective());
        username.Append(GetRandomName());
        username.Append(GetRandomDigits(_randomDigitCount));
        return username.ToString();
    }

    private int GetRandomDigits(int randomDigitCount)
    {
        var startRange = Convert.ToInt32(Math.Pow(10, randomDigitCount - 1));
        var endRange = Convert.ToInt32(Math.Pow(10, randomDigitCount));
        return _random.Next(startRange, endRange);
    }

    private string GetRandomName()
    {
        var randomIndex = _random.Next(_names.Length);
        return _names[randomIndex];
    }

    private string GetRandomAdjective()
    {
        var randomIndex = _random.Next(_adjectives.Length);
        return _adjectives[randomIndex];
    }
}
