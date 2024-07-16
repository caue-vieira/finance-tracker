using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finance_tracker.Models;

public class User {
    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(150)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(150)]
    public string? Usuario { get; set; }

    [Required]
    [StringLength(150)]
    public string? Senha { get; set; }
}
