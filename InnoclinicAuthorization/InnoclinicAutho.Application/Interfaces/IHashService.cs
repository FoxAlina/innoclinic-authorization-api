namespace InnoclinicAutho.Application.Interfaces;

public interface IHashService
{
    public string GetHash(string input);
    public bool VerifyString(string input, string hashedInput);
}

