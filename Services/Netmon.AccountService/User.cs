using Microsoft.AspNetCore.Identity;

namespace Netmon.AccountService;

public class User : IdentityUser<int>
{
    [PersonalData]
    public string FullName { get; set; }

    [PersonalData]
    public string ProfileImageName { get; set; }
}