﻿// <auto-generated />
using System;
using LCPMauiWebApi.Server.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LCPMauiWebApi.Server.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LCPMauiWebApi.Server.Classes.Attachments", b =>
                {
                    b.Property<int?>("AttachsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("AttachsId"));

                    b.Property<DateTime?>("DateFileDeleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateFileModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateFileUploaded")
                        .HasColumnType("datetime2");

                    b.Property<string>("File")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("bit");

                    b.Property<int?>("PostsId")
                        .HasColumnType("int");

                    b.Property<string>("Privacy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReactionsId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("AttachsId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("LCPMauiWebApi.Server.Classes.Comments", b =>
                {
                    b.Property<int?>("CommentsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("CommentsId"));

                    b.Property<int?>("AttachsId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PostsId")
                        .HasColumnType("int");

                    b.Property<string>("Privacy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReactionsId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("CommentsId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("LCPMauiWebApi.Server.Classes.Feedback", b =>
                {
                    b.Property<int?>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("FeedbackId"));

                    b.Property<string>("AttachmentFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Counter")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("LCPMauiWebApi.Server.Classes.MyUsers", b =>
                {
                    b.Property<int?>("MyUsersId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("MyUsersId"));

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cover")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Privacy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MyUsersId");

                    b.ToTable("MyUsers");
                });

            modelBuilder.Entity("LCPMauiWebApi.Server.Classes.MyUsersAuth", b =>
                {
                    b.Property<int?>("MyUsersAuthId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("MyUsersAuthId"));

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateExpLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MyUsersAuthId");

                    b.ToTable("MyUsersAuth");
                });

            modelBuilder.Entity("LCPMauiWebApi.Server.Classes.Posts", b =>
                {
                    b.Property<int?>("PostsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("PostsId"));

                    b.Property<int?>("AttachsId")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cover")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsFeatured")
                        .HasColumnType("bit");

                    b.Property<string>("Privacy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReactionsId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("PostsId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("LCPMauiWebApi.Server.Classes.Reactions", b =>
                {
                    b.Property<int?>("ReactionsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ReactionsId"));

                    b.Property<int?>("AttachsId")
                        .HasColumnType("int");

                    b.Property<int?>("CommentsId")
                        .HasColumnType("int");

                    b.Property<int?>("Counter")
                        .HasColumnType("int");

                    b.Property<int?>("PostsId")
                        .HasColumnType("int");

                    b.Property<int?>("RepliesId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("ReactionsId");

                    b.ToTable("Reactions");
                });

            modelBuilder.Entity("LCPMauiWebApi.Server.Classes.Replies", b =>
                {
                    b.Property<int?>("RepliesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("RepliesId"));

                    b.Property<int?>("AttachsId")
                        .HasColumnType("int");

                    b.Property<int?>("CommentsId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PostsId")
                        .HasColumnType("int");

                    b.Property<string>("Privacy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReactionsId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("RepliesId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("LCPMauiWebApi.Server.Classes.Todo", b =>
                {
                    b.Property<int?>("TodoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("TodoId"));

                    b.Property<DateTime?>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("TodoId");

                    b.ToTable("Todo");
                });
#pragma warning restore 612, 618
        }
    }
}
