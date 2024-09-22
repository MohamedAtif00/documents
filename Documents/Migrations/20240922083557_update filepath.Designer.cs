﻿// <auto-generated />
using System;
using Documents.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Documents.Migrations
{
    [DbContext(typeof(DocumentDbContext))]
    [Migration("20240922083557_update filepath")]
    partial class updatefilepath
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Documents.Model.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SignedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Documents");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Comments = "[\"comment\"]",
                            FilePath = "/docs/Document1.pdf",
                            Name = "Document1.pdf",
                            Status = "Reviewed"
                        },
                        new
                        {
                            Id = 2,
                            Comments = "[\"comment\"]",
                            FilePath = "/docs/Document2.pdf",
                            Name = "Document2.pdf",
                            Status = "Signed"
                        },
                        new
                        {
                            Id = 3,
                            Comments = "[\"comment\"]",
                            FilePath = "/docs/Document3.pdf",
                            Name = "Document3.pdf",
                            Status = "Hold"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
