using System.ComponentModel.DataAnnotations;

namespace AccountService.Entities;

public class Session(Guid userId)
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public Guid UserId { get; set; } = userId;

    [Required]
    [MaxLength(64)]
    public string Token { get; set; } = GetRandomSessionToken();

    [Required]
    public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddDays(7);

    public User? User { get; set; }
    
    private static readonly Random Random = new();
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    
    static string GetRandomSessionToken()
    {
        return new string(Enumerable.Repeat(Chars, 64)
            .Select(chars => chars[Random.Next(chars.Length)])
            .ToArray());
    }
}