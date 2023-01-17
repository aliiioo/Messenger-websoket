using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Multi_Shop.Context.Entites
{
    public class Shop
    {
        [Key]
        public int ShopId { get; set; }
        [Required]
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public ShopGroup ShopGroup { get; set; }

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
        public int Like { get; set; }
        [MaxLength(30)]
        public string Color { get; set; }
        [MaxLength(10)]
        public string Size { get; set; }


        public List<Likes> Likes{ get; set; }
        public List<ProductComment> Comments { get; set; }




    }
}
