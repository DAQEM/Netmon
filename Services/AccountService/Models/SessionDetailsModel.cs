namespace AccountService.Models;

public class SessionDetailsModel
{
    public string Id { get; set; } = null!;
    public DateTime Expires { get; set; }
}