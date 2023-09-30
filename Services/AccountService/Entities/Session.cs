using System.ComponentModel.DataAnnotations;

namespace AccountService.Entities;

public class Session
{
    public Session(Guid userId)
    {
        UserId = userId;
        Token = GetRandomSessionToken();
        ExpirationDate = DateTime.UtcNow.AddDays(7);
    }

    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    [MaxLength(64)]
    public string Token { get; set; }
    
    [Required]
    public DateTime ExpirationDate { get; set; }
    
    public User User { get; set; }
    
    private static readonly Random Random = new();
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    
    static string GetRandomSessionToken()
    {
        return new string(Enumerable.Repeat(Chars, 64)
            .Select(chars => chars[Random.Next(chars.Length)])
            .ToArray());
    }
}