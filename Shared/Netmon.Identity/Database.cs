using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Netmon.Identity;

public class Database : IdentityDbContext<User>
{
    public Database(DbContextOptions options) : base(options)
    {
    }
}