using Microsoft.AspNetCore.Identity;

namespace IdentityAuth.Models
{
    public class AppUser : IdentityUser, IAuditable
    {
        public string FullName { get; set; }
        public string Status { get; set; }
        public int Age { get; set; }

        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset ModifiedDate { get; set; }
        public DateTimeOffset DeletedDate { get; set; }
        public bool isDeleted { get; set; } = false;

    }
}
