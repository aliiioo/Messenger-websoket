using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Multi_Shop.Context.Entites
{
    public class UserList
    {

        public UserList()
        {

        }

        [Key]
        public int idd { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }

        public List<Likes> Likes { get; set; }

    }
}
