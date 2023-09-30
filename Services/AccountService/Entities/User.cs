using System.ComponentModel.DataAnnotations;

namespace AccountService.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(25)]
    public string Username { get; set; } = null!;
    
    [Required]
    [MaxLength(320)]
    public string Email { get; set; } = null!;
    
    [Required]
    [MaxLength(64)]
    public string Password { get; set; } = null!;
    
    [Required]
    [MaxLength(64)]
    public string Salt { get; set; } = null!;

    public List<Role> Roles { get; set; } = new();

    public List<Session> Sessions { get; set; } = new();
}