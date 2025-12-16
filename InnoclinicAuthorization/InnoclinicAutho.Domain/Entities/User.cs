using InnoclinicAutho.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace InnoclinicAutho.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? DateUpdated { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
        public Guid UserID { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
