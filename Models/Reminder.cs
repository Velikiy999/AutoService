using System;
using System.ComponentModel.DataAnnotations;

namespace AutoService.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public Client? Client { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReminderDate { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
