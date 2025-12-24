namespace InnoclinicAutho.Domain.Common;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public interface IBaseEntity
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }
	public DateTimeOffset CreateDateTime { get; set; }
	public DateTimeOffset? UpdateDateTime { get; set; }
	public DateTimeOffset? DeleteDateTime { get; set; }
	public Guid UserId { get; set; }
	public bool IsDeleted { get; set; }
}

