using FluentValidation;
using InnoclinicAutho.Application.Interfaces;

namespace InnoclinicAutho.Application.UseCases.Users.Queries.GetUser;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator(IUser user)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User Id is required.")
            .Must(x => user.Roles.Contains("Admin") || x == user.Id)
            .WithMessage("Current user id does not match the request user id.");
    }
}
