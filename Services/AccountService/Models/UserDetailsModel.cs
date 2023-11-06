namespace AccountService.Models;

public class UserDetailsModel
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<string> Roles { get; set; } = null!;
    public SessionDetailsModel Session { get; set; } = null!;
}