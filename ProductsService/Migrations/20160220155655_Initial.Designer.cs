using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using ProductsService.Model;

namespace ProductsService.Migrations
{
    [DbContext(typeof(ProductsContext))]
    [Migration("20160220155655_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("ProductsService.Model.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<decimal>("GrossPrice");

                    b.Property<string>("Name");

                    b.Property<decimal>("NetPrice");

                    b.Property<decimal>("Stock");

                    b.HasKey("Id");
                });
        }
    }
}
