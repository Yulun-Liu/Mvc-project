using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1121726Final.Models
{
    public class Concert
    {
        [Key]
        [Required]
        [Display(Name = "演唱會編號")]
        public int ConcertId { get; set; }
        [Required]
        [ForeignKey("GroupId")]
        [Display(Name = "團體編號")]
        public int GroupId { get; set; }
        public Group Group { get; set; }

        [Display(Name = "演唱會名稱")]
        public string ConcertName { get; set; }
        [Required]
        [Display(Name = "演唱會日期")]
        public DateTime Date { get; set; }
        [Display(Name = "場地")]
        [Required]
        [StringLength(200)]
        public string Venue { get; set; }
        [Display(Name = "所有座位")]
        public int TotalSeats { get; set; }
        [Display(Name ="剩餘座位")]
        public int RemainingSeats => TotalSeats - Ticket.Count(t => t.IsPurchased);
        public List<Ticket> Ticket { get; set; }

    }
}
