using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Netmon.AccountService;

public class Database(DbContextOptions options) : IdentityDbContext<User>(options);