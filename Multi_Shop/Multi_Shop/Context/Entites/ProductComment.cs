using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Multi_Shop.Context.Entites
{
    public class ProductComment
    {
        public ProductComment()
        {

        }
        [Key]
        public int CommentId { get; set; }
       
        public int ProductId{ get; set; }
        public string UserName { get; set; }

        [MaxLength(700)]
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }

        [ForeignKey("ProductId")]
        public Shop Shop{ get; set; }
    }
}
