using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Multi_Shop.Context.Entites
{
 
    public class Likes
    {


        public Likes()
        {

        }

        [Key]
        public int LikeId { get; set; }
        [Required]
        public int ShopId { get; set; }

        public string UserName1 { get; set; }

        [Required]
        public int UserId { get; set; }
        
        [Required]
        public bool Like { get; set; }
        public DateTime LikeDate { get; set; } = DateTime.Now;
 
        [ForeignKey("ShopId")]
        public Shop shop { get; set; }

        
    }

}
