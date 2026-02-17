using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.UseCases.Users.AuthAttributes;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;
using System.Reflection;

namespace InnoclinicAutho.Application.UseCases.Behavior;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IUser _user;
    
    public AuthorizationBehaviour(IUser user)
    {
        _user = user;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeRolesAttribute>();

        if (authorizeAttributes.Any())
        {
            if (_user.Id == null)
            {
                throw new UnauthorizedAccessException();
            }

            var authorizeAttributesWithRoles = authorizeAttributes.Where(a => a.Roles.Any());

            if (authorizeAttributesWithRoles.Any())
            {
                var authorized = false;

                foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles))
                {
                    foreach (var role in roles)
                    {
                        var isInRole = _user.Roles?.Any(x => role.ToString() == x) ?? false;

                        if (isInRole)
                        {
                            authorized = true;
                            break;
                        }
                    }
                }

                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}
