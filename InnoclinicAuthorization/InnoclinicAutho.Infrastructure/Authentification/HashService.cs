namespace InnoclinicAutho.Infrastructure.Authentification;

using InnoclinicAutho.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

public class HashService : IHashService
{
    private readonly IPasswordHasher<object> _passwordHasher;

    public HashService()
    {
        _passwordHasher = new PasswordHasher<object>();
    }

    public string GetHash(string input)
    {
        return _passwordHasher.HashPassword(new object(), input);
    }

    public bool VerifyString(string input, string hashedInput)
    {
        var result = _passwordHasher.VerifyHashedPassword(new object(), hashedInput, input);

        return result == PasswordVerificationResult.Success ||
               result == PasswordVerificationResult.SuccessRehashNeeded;
    }
}
