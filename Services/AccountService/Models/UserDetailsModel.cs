namespace AccountService.Models;

public class UserDetailsModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public SessionDetailsModel Session { get; set; }
}