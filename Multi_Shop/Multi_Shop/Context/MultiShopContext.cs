using Microsoft.EntityFrameworkCore;
using Multi_Shop.Context.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multi_Shop.Context
{
    public class MultiShopContext : DbContext
    {
        public MultiShopContext(DbContextOptions<MultiShopContext> options) : base(options)
        {

        }

        public DbSet<ShopGroup> ShopGroups { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopGroup>Shopgroups{ get; set; }
        public DbSet<Likes>Likes{ get; set; }
        public DbSet<UserList>Usersss{ get; set; }
        public DbSet<ProductComment>Comments{ get; set; }
        public DbSet<CartIidd> CartI { get; set; }
        public DbSet<ProductsAdmins> ProductsOfAdmins { get; set; }
        public DbSet<UsersGroup_chanel> usersGroup_Chanels{ get; set; }
        public DbSet<UpoladFile> upoladFiles { get; set; }
        public DbSet<message> messages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopGroup>()
                .HasQueryFilter(g => !g.IsDelete);

            base.OnModelCreating(modelBuilder);


        }

    }
}
