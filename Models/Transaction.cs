using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finance_tracker.Models {
    public class Transaction {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Column("data_transacao")]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("tipo")]
        public string? Type { get; set; }

        [Required]
        [Column("quantidade")]
        public float Amount {  get; set; }

        [Required]
        [Column("user_id")]
        public Guid UserId { get; set; }
    }
}
