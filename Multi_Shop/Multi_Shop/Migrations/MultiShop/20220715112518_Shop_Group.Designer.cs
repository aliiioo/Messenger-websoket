﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Multi_Shop.Context;

namespace Multi_Shop.Migrations.MultiShop
{
    [DbContext(typeof(MultiShopContext))]
    [Migration("20220715112518_Shop_Group")]
    partial class Shop_Group
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Multi_Shop.Context.Entites.Shop", b =>
                {
                    b.Property<int>("ShopId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShopImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShopTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ShopId");

                    b.HasIndex("GroupId");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("Multi_Shop.Context.Entites.ShopGroup", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("GroupId");

                    b.HasIndex("ParentId");

                    b.ToTable("ShopGroup");
                });

            modelBuilder.Entity("Multi_Shop.Context.Entites.Shop", b =>
                {
                    b.HasOne("Multi_Shop.Context.Entites.ShopGroup", "ShopGroup")
                        .WithMany("Shop")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShopGroup");
                });

            modelBuilder.Entity("Multi_Shop.Context.Entites.ShopGroup", b =>
                {
                    b.HasOne("Multi_Shop.Context.Entites.ShopGroup", null)
                        .WithMany("ShopGroups")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Multi_Shop.Context.Entites.ShopGroup", b =>
                {
                    b.Navigation("Shop");

                    b.Navigation("ShopGroups");
                });
#pragma warning restore 612, 618
        }
    }
}