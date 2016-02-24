using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using DefaultPhotoProvider;

namespace DefaultPhotoProvider.Migrations
{
    [DbContext(typeof(PhotoProviderContext))]
    partial class PhotoProviderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
