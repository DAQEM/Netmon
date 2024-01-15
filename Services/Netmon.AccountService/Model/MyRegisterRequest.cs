namespace Netmon.AccountService.Model;

public class MyRegisterRequest
{
    public required string UserName { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public required string ProfileImageName { get; init; }
    public required string Password { get; init; }
}