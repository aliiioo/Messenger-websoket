using System.ComponentModel.DataAnnotations;

namespace Multi_Shop.Context.Entites
{
    public class message
    {
        [Key]
        public int id { get; set; }
        [MaxLength(100)]
        public string username { get; set; }
        public string messageing { get; set; }
    }
}
