using System;
using System.ComponentModel.DataAnnotations;

namespace AutoService.Models
{
    public class FinancialTransactions
    {
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        public Client? Client { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Type { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}
