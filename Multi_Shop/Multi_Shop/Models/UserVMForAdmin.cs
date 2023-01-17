namespace Multi_Shop.Models
{
    public class UserVMForAdmin
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int Num_Role { get; set; } = 0;
        public bool Picture { get; set; } = false;  
    }
}
