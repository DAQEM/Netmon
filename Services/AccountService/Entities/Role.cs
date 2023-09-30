using System.ComponentModel.DataAnnotations;

namespace AccountService.Entities;

public class Role
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(25)]
    public string Name { get; set; } = null!;
    
    public List<User> Users { get; set; } = new();
}