namespace InnoclinicAutho.Domain.Exceptions;

public class MissingRoleException : DomainException
{
    public MissingRoleException(string email, string userRole) : base($"User '{email}' is missing the required role: {userRole}.") { }
}
