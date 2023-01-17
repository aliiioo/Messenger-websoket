using System.ComponentModel.DataAnnotations;

namespace Multi_Shop.Context.Entites
{
    public class ProductsAdmins
    {
        [Key]
        public int id { get; set; }
        [MaxLength(100)]
        public int ProId { get; set; }
        [MaxLength(100)]
        public string Username { get; set; }
    }
}
