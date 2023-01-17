using System.ComponentModel.DataAnnotations;

namespace Multi_Shop.Context.Entites
{
    public class UsersGroup_chanel
    {
        [Key]
        public int id { get; set; }
        [MaxLength(100)]
        public string Username { get; set; }
        [MaxLength(100)]
        public int ProId { get; set; }
        

    }
}
