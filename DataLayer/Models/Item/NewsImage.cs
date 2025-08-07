using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Item
{
    public class NewsImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NewsId { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImagePath { get; set; }

        public News News { get; set; }
    }
}
