﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeHub.Infrastructure.EfStructures;

namespace RecipeHub.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220525095103_Ingredients_seed")]
    partial class Ingredients_seed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RecipeHub.Infrastructure.DBO.ArticleDbo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("RecipeHub.Infrastructure.DBO.CommentDbo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Rating")
                        .HasColumnType("bigint");

                    b.Property<Guid>("RecipeDboId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RecipeDboId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("RecipeHub.Infrastructure.DBO.IngredientDbo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CaloriesPerUnit")
                        .HasColumnType("int");

                    b.Property<int>("MeasureUnit")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("RecipeHub.Infrastructure.DBO.RecipeDbo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instructions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PreparationTime")
                        .HasColumnType("bigint");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("RecipeHub.Infrastructure.DBO.CommentDbo", b =>
                {
                    b.HasOne("RecipeHub.Infrastructure.DBO.RecipeDbo", "RecipeDbo")
                        .WithMany("CommentsDbo")
                        .HasForeignKey("RecipeDboId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RecipeDbo");
                });

            modelBuilder.Entity("RecipeHub.Infrastructure.DBO.RecipeDbo", b =>
                {
                    b.OwnsMany("RecipeHub.Infrastructure.DBO.RecipeIngredientDbo", "RecipeIngredientsDbo", b1 =>
                        {
                            b1.Property<Guid>("RecipeDboId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<Guid>("IngredientDboId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Quantity")
                                .HasColumnType("int");

                            b1.HasKey("RecipeDboId", "Id");

                            b1.HasIndex("IngredientDboId");

                            b1.ToTable("RecipeIngredientDbos");

                            b1.HasOne("RecipeHub.Infrastructure.DBO.IngredientDbo", "IngredientDbo")
                                .WithMany()
                                .HasForeignKey("IngredientDboId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("RecipeDboId");

                            b1.Navigation("IngredientDbo");
                        });

                    b.Navigation("RecipeIngredientsDbo");
                });

            modelBuilder.Entity("RecipeHub.Infrastructure.DBO.RecipeDbo", b =>
                {
                    b.Navigation("CommentsDbo");
                });
#pragma warning restore 612, 618
        }
    }
}
