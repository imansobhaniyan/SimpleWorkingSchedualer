﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleWorkingSchedualer.DataAccessLayer;

namespace SimpleWorkingSchedualer.DataAccessLayer.Migrations
{
    [DbContext(typeof(SimpleWorkingSchedualerDbContext))]
    partial class SimpleWorkingSchedualerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SimpleWorkingSchedualer.StorageModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SimpleWorkingSchedualer.StorageModels.UserTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TaskDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTask");
                });

            modelBuilder.Entity("SimpleWorkingSchedualer.StorageModels.UserTaskStatusHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UserTaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserTaskId");

                    b.ToTable("UserTaskStatusHistory");
                });

            modelBuilder.Entity("SimpleWorkingSchedualer.StorageModels.UserTask", b =>
                {
                    b.HasOne("SimpleWorkingSchedualer.StorageModels.User", "User")
                        .WithMany("UserTasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SimpleWorkingSchedualer.StorageModels.UserTaskStatusHistory", b =>
                {
                    b.HasOne("SimpleWorkingSchedualer.StorageModels.UserTask", "UserTask")
                        .WithMany("UserTaskStatusHistories")
                        .HasForeignKey("UserTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserTask");
                });

            modelBuilder.Entity("SimpleWorkingSchedualer.StorageModels.User", b =>
                {
                    b.Navigation("UserTasks");
                });

            modelBuilder.Entity("SimpleWorkingSchedualer.StorageModels.UserTask", b =>
                {
                    b.Navigation("UserTaskStatusHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
