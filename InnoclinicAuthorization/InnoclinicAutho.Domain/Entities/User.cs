using InnoclinicAutho.Domain.Common;

namespace InnoclinicAutho.Domain.Entities
{
    public class User: BaseEntity
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
