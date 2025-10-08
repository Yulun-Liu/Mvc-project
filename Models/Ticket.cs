using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1121726Final.Models
{
    public class Ticket
    {
        [Key]
        [Required]
        [Display(Name ="票號")]
        public int TicketId { get; set; }
        [Required]
        [Display(Name ="持有者")]
        public string Owner { get; set; }
        [Required]
        [ForeignKey("ConcertId")]
        [Display(Name = "演唱會編號")]
        public int ConcertId { get; set; }
        public Concert Concert { get; set; }

        [Display(Name = "購買日期")]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        [Display(Name = "是否已購買")]
        public bool IsPurchased { get; set; } = false;
    }
}
