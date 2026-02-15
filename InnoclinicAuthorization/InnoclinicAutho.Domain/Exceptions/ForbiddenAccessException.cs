namespace InnoclinicAutho.Domain.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base("Action is forbidden.") { }
}
