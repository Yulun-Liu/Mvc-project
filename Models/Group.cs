using System.ComponentModel.DataAnnotations;

namespace _1121726Final.Models
{
    public class Group
    {
        [Key]
        [Required]
        [Display(Name = "團體編號")]
        public int GroupId { get; set; }
        [Display(Name = "團名")]
        public string Name { get; set; }
        [Display(Name = "簡介")]
        public string Description { get; set; }
        [Display(Name = "圖片")]
        public string ImagePath { get; set; }
        public Concert Concert { get; set; }

    }
}
