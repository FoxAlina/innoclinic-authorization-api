namespace InnoclinicAutho.Domain.Common;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public DateTimeOffset CreateDateTime { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdateDateTime { get; set; }
    public DateTimeOffset? DeleteDateTime { get; set; }
    public Guid CreatorUserId { get; set; }
    public bool IsDeleted { get; set; } = false;
}

