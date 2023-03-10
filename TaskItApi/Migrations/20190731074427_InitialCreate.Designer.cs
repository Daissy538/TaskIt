// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskItApi.Models;

namespace TaskItApi.Migrations
{
    [DbContext(typeof(TaskItDbContext))]
    [Migration("20190731074427_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaskItApi.Entities.Group", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Names");

                    b.HasKey("ID");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("TaskItApi.Entities.Task", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("From");

                    b.Property<int>("GroupID");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<DateTime>("Until");

                    b.HasKey("ID");

                    b.HasIndex("GroupID");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskItApi.Entities.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<int?>("GroupID");

                    b.Property<string>("Nickname");

                    b.Property<string>("Password");

                    b.HasKey("ID");

                    b.HasIndex("GroupID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskItApi.Entities.Task", b =>
                {
                    b.HasOne("TaskItApi.Entities.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaskItApi.Entities.User", b =>
                {
                    b.HasOne("TaskItApi.Entities.Group")
                        .WithMany("Members")
                        .HasForeignKey("GroupID");
                });
#pragma warning restore 612, 618
        }
    }
}
