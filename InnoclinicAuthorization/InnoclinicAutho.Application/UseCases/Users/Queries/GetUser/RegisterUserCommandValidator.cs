using FluentValidation;
using InnoclinicAutho.Application.Interfaces;

namespace InnoclinicAutho.Application.UseCases.Users.Queries.GetUser;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    private readonly IUser _user;

    public GetUserQueryValidator(IUser user)
    {
        _user = user;
    }

    public GetUserQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User Id is required.")
            .Equal(_user.Id).WithMessage("Current user id does not match the request user id.");
    }
}
