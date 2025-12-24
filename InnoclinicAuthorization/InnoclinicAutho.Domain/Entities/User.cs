namespace InnoclinicAutho.Domain.Entities;

using InnoclinicAutho.Domain.Common;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<Guid>, IBaseEntity
{
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public DateTimeOffset CreateDateTime { get; set; } = DateTimeOffset.UtcNow;
	public DateTimeOffset? UpdateDateTime { get; set; }
	public DateTimeOffset? DeleteDateTime { get; set; }
	public Guid UserId { get; set; }
	public bool IsDeleted { get; set; } = false;
}
