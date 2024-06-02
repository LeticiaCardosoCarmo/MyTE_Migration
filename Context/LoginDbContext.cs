using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyTE_Migration.Context
{
    public class LoginDbContext : IdentityDbContext
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options) { }
    }
}
