using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Multi_Shop.Context.Entites
{
    public class CartIidd
    {
        [Key]
        public int CarttId { get; set; }
        [MaxLength(120)]
        public string UserName { get; set; }
        [MaxLength(30)]
        public string ProductName { get; set; }
        [MaxLength(20)]
        public string  Sizing { get; set; }
        [MaxLength(20)]
        public string Coloring { get; set; }
        public int Numbers { get; set; }
        [MaxLength(20)]
        public string Price { get; set; }
        public string ImgName { get; set; }


    }
}
