﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Persistence.Context;

#nullable disable

namespace Persistence.Migrations
{
   [DbContext(typeof(PizzaContext))]
    [Migration("20230810185107_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.21");

            modelBuilder.Entity("Domain.Models.Pizza", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SauceId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SauceId");

                    b.ToTable("Pizzas");
                });

            modelBuilder.Entity("Domain.Models.Sauce", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sauces");
                });

            modelBuilder.Entity("Domain.Models.Topping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PizzaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PizzaId");

                    b.ToTable("Toppings");
                });

            modelBuilder.Entity("Domain.Models.Pizza", b =>
                {
                    b.HasOne("Domain.Models.Sauce", "Sauce")
                        .WithMany()
                        .HasForeignKey("SauceId");

                    b.Navigation("Sauce");
                });

            modelBuilder.Entity("Domain.Models.Topping", b =>
                {
                    b.HasOne("Domain.Models.Pizza", null)
                        .WithMany("Toppings")
                        .HasForeignKey("PizzaId");
                });

            modelBuilder.Entity("Domain.Models.Pizza", b =>
                {
                    b.Navigation("Toppings");
                });
#pragma warning restore 612, 618
        }
    }
}