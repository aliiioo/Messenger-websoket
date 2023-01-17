using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class AddProductVM
    {
        [Required]
        [MaxLength(100)]
        public string ShopTitle { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string Price { get; set; }
        public string Tag { get; set; }
        public string ShopImg { get; set; }
        public DateTime CreatDate { get; set; }
        public int Number { get; set; }
        public int GroupId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }

    }
}
