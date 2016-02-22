using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using PhotoProvider;

namespace PhotoProvider.Migrations
{
    [DbContext(typeof(PhotoProviderContext))]
    [Migration("20160221091829_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("PhotoProvider.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<string>("Path");

                    b.Property<int>("Size");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("PhotoProvider.ProviderClient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ObjectId");

                    b.Property<int>("ObjectTypeId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("PhotoProvider.Photo", b =>
                {
                    b.HasOne("PhotoProvider.ProviderClient")
                        .WithMany()
                        .HasForeignKey("ClientId");
                });
        }
    }
}
