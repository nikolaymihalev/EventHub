﻿// <auto-generated />
using System;
using EventHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventHub.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241118163035_Seed")]
    partial class Seed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventHub.Infrastructure.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Category's identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasComment("Category's name");

                    b.HasKey("Id");

                    b.ToTable("Categories", t =>
                        {
                            t.HasComment("Represents a category");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Technology"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Music"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Art"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Sports"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Education"
                        });
                });

            modelBuilder.Entity("EventHub.Infrastructure.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Comment's identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasComment("Comment's content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasComment("Comment's creation date");

                    b.Property<int>("EventId")
                        .HasColumnType("int")
                        .HasComment("Event's identifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("User's identifier");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments", t =>
                        {
                            t.HasComment("Represents a comment");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Great event!",
                            CreatedAt = new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventId = 2,
                            UserId = "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc"
                        },
                        new
                        {
                            Id = 2,
                            Content = "Looking forward to it!",
                            CreatedAt = new DateTime(2024, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventId = 1,
                            UserId = "52569acc-8c9f-4e79-86ef-d55071e0be3d"
                        },
                        new
                        {
                            Id = 3,
                            Content = "This sounds amazing!",
                            CreatedAt = new DateTime(2024, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventId = 4,
                            UserId = "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40"
                        },
                        new
                        {
                            Id = 4,
                            Content = "I am going to join this!",
                            CreatedAt = new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventId = 5,
                            UserId = "92b2c75a-ab33-425a-be05-6d6f7af74a5a"
                        },
                        new
                        {
                            Id = 5,
                            Content = "Excited to attend!",
                            CreatedAt = new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventId = 3,
                            UserId = "f593b51d-ef90-4914-9099-3188302616dc"
                        });
                });

            modelBuilder.Entity("EventHub.Infrastructure.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Event's identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasComment("Category's identifier");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("Creator's identifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasComment("Event's date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasComment("Event's description");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Event's location");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("Event's title");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Events", t =>
                        {
                            t.HasComment("Represents an event");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            CreatorId = "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc",
                            Date = new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A conference on the latest in technology",
                            Location = "Los Angeles, CA",
                            Title = "Tech Conference"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            CreatorId = "52569acc-8c9f-4e79-86ef-d55071e0be3d",
                            Date = new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Live rock music concert",
                            Location = "New York, NY",
                            Title = "Rock Concert"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            CreatorId = "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40",
                            Date = new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "New art exhibition opening",
                            Location = "Paris, France",
                            Title = "Art Gallery Opening"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 4,
                            CreatorId = "92b2c75a-ab33-425a-be05-6d6f7af74a5a",
                            Date = new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Exciting football competition",
                            Location = "London, UK",
                            Title = "Football Tournament"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 5,
                            CreatorId = "52569acc-8c9f-4e79-86ef-d55071e0be3d",
                            Date = new DateTime(2024, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A webinar about online learning",
                            Location = "Virtual",
                            Title = "Online Education Webinar"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 1,
                            CreatorId = "f593b51d-ef90-4914-9099-3188302616dc",
                            Date = new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Seminar on AI and machine learning",
                            Location = "San Francisco, CA",
                            Title = "AI and Machine Learning Seminar"
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 2,
                            CreatorId = "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40",
                            Date = new DateTime(2024, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Enjoy a night of live jazz music",
                            Location = "Chicago, IL",
                            Title = "Jazz Music Night"
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 3,
                            CreatorId = "92b2c75a-ab33-425a-be05-6d6f7af74a5a",
                            Date = new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Explore the world of modern art",
                            Location = "Berlin, Germany",
                            Title = "Modern Art Exhibition"
                        },
                        new
                        {
                            Id = 9,
                            CategoryId = 4,
                            CreatorId = "f593b51d-ef90-4914-9099-3188302616dc",
                            Date = new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Annual basketball competition",
                            Location = "Los Angeles, CA",
                            Title = "Basketball Championship"
                        },
                        new
                        {
                            Id = 10,
                            CategoryId = 5,
                            CreatorId = "52569acc-8c9f-4e79-86ef-d55071e0be3d",
                            Date = new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Intensive coding bootcamp",
                            Location = "Austin, TX",
                            Title = "Coding Bootcamp"
                        },
                        new
                        {
                            Id = 11,
                            CategoryId = 1,
                            CreatorId = "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40",
                            Date = new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Workshop for web development",
                            Location = "Online",
                            Title = "Web Development Workshop"
                        },
                        new
                        {
                            Id = 12,
                            CategoryId = 2,
                            CreatorId = "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc",
                            Date = new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Exciting pop concert",
                            Location = "Miami, FL",
                            Title = "Pop Concert"
                        },
                        new
                        {
                            Id = 13,
                            CategoryId = 3,
                            CreatorId = "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc",
                            Date = new DateTime(2024, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Workshop on digital art techniques",
                            Location = "Amsterdam, Netherlands",
                            Title = "Digital Art Workshop"
                        },
                        new
                        {
                            Id = 14,
                            CategoryId = 4,
                            CreatorId = "52569acc-8c9f-4e79-86ef-d55071e0be3d",
                            Date = new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Relax and rejuvenate with yoga",
                            Location = "Bali, Indonesia",
                            Title = "Yoga Retreat"
                        },
                        new
                        {
                            Id = 15,
                            CategoryId = 5,
                            CreatorId = "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40",
                            Date = new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Explore the world of online learning",
                            Location = "Virtual",
                            Title = "Online Learning Expo"
                        });
                });

            modelBuilder.Entity("EventHub.Infrastructure.Models.EventRegistration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Registration's identifier");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EventId")
                        .HasColumnType("int")
                        .HasComment("Event's identifier");

                    b.Property<DateTime>("RegisteredAt")
                        .HasColumnType("datetime2")
                        .HasComment("Registration date");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasComment("User's identifier");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("EventRegistrations", t =>
                        {
                            t.HasComment("Represents a user's registration for an event");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EventId = 1,
                            RegisteredAt = new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc"
                        },
                        new
                        {
                            Id = 2,
                            EventId = 2,
                            RegisteredAt = new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "52569acc-8c9f-4e79-86ef-d55071e0be3d"
                        },
                        new
                        {
                            Id = 3,
                            EventId = 3,
                            RegisteredAt = new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40"
                        },
                        new
                        {
                            Id = 4,
                            EventId = 4,
                            RegisteredAt = new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "92b2c75a-ab33-425a-be05-6d6f7af74a5a"
                        },
                        new
                        {
                            Id = 5,
                            EventId = 5,
                            RegisteredAt = new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "f593b51d-ef90-4914-9099-3188302616dc"
                        },
                        new
                        {
                            Id = 6,
                            EventId = 6,
                            RegisteredAt = new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc"
                        },
                        new
                        {
                            Id = 7,
                            EventId = 7,
                            RegisteredAt = new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "52569acc-8c9f-4e79-86ef-d55071e0be3d"
                        },
                        new
                        {
                            Id = 8,
                            EventId = 8,
                            RegisteredAt = new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40"
                        },
                        new
                        {
                            Id = 9,
                            EventId = 9,
                            RegisteredAt = new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "92b2c75a-ab33-425a-be05-6d6f7af74a5a"
                        },
                        new
                        {
                            Id = 10,
                            EventId = 10,
                            RegisteredAt = new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "f593b51d-ef90-4914-9099-3188302616dc"
                        },
                        new
                        {
                            Id = 11,
                            EventId = 11,
                            RegisteredAt = new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc"
                        },
                        new
                        {
                            Id = 12,
                            EventId = 12,
                            RegisteredAt = new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "52569acc-8c9f-4e79-86ef-d55071e0be3d"
                        },
                        new
                        {
                            Id = 13,
                            EventId = 13,
                            RegisteredAt = new DateTime(2024, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40"
                        },
                        new
                        {
                            Id = 14,
                            EventId = 14,
                            RegisteredAt = new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "92b2c75a-ab33-425a-be05-6d6f7af74a5a"
                        },
                        new
                        {
                            Id = 15,
                            EventId = 15,
                            RegisteredAt = new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = "f593b51d-ef90-4914-9099-3188302616dc"
                        });
                });

            modelBuilder.Entity("EventHub.Infrastructure.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasComment("User's identifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasComment("User's creation date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("User's email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("User's first name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("User's last name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("User's password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("User's username");

                    b.HasKey("Id");

                    b.ToTable("Users", t =>
                        {
                            t.HasComment("Represents a user");
                        });

                    b.HasData(
                        new
                        {
                            Id = "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc",
                            CreatedAt = new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "john.doe@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            PasswordHash = "AQAAAAIAAYagAAAAEHJL24YHOSxiq4VLglJpxUvkAFDR785wLZCWxyHO4qEOB5vjfD7lqMomVfqXm0ziXQ==",
                            Username = "john_doe"
                        },
                        new
                        {
                            Id = "52569acc-8c9f-4e79-86ef-d55071e0be3d",
                            CreatedAt = new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jane.smith@example.com",
                            FirstName = "Jane",
                            LastName = "Smith",
                            PasswordHash = "AQAAAAIAAYagAAAAENkjlyvjVLiWCmUCQYswkHhjKGVRkZoqzJmLdzHRVCjdGXxNVqAHTQc0AaxkxBJVMA==",
                            Username = "jane_smith"
                        },
                        new
                        {
                            Id = "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40",
                            CreatedAt = new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mike.jones@example.com",
                            FirstName = "Mike",
                            LastName = "Jones",
                            PasswordHash = "AQAAAAIAAYagAAAAEGoOfPaPkYDr8E2NlcVSz0tvfOpE8461ckK+cbeDeCT+otUYz8pMaAQ1OaEndom8Uw==",
                            Username = "mike_jones"
                        },
                        new
                        {
                            Id = "92b2c75a-ab33-425a-be05-6d6f7af74a5a",
                            CreatedAt = new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "lucy.brown@example.com",
                            FirstName = "Lucy",
                            LastName = "Brown",
                            PasswordHash = "AQAAAAIAAYagAAAAELrFIh5XRSZpO61TGmp6yE3hoQzj9p1SRE4SvMIEC53rdJghrOUGjRW5k1CNGWgCGA==",
                            Username = "lucy_brown"
                        },
                        new
                        {
                            Id = "f593b51d-ef90-4914-9099-3188302616dc",
                            CreatedAt = new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "susan.lee@example.com",
                            FirstName = "Susan",
                            LastName = "Lee",
                            PasswordHash = "AQAAAAIAAYagAAAAEAoeQFglNWH45aAsqOg6q6/DozYW9jaWuxOUYD3Nhxu757GaLAbOYSTrEWh1oUlUSQ==",
                            Username = "susan_lee"
                        });
                });

            modelBuilder.Entity("EventHub.Infrastructure.Models.Comment", b =>
                {
                    b.HasOne("EventHub.Infrastructure.Models.Event", "Event")
                        .WithMany("Comments")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventHub.Infrastructure.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventHub.Infrastructure.Models.Event", b =>
                {
                    b.HasOne("EventHub.Infrastructure.Models.Category", "Category")
                        .WithMany("Events")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventHub.Infrastructure.Models.User", "Creator")
                        .WithMany("CreatedEvents")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("EventHub.Infrastructure.Models.EventRegistration", b =>
                {
                    b.HasOne("EventHub.Infrastructure.Models.Event", "Event")
                        .WithMany("Registrations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventHub.Infrastructure.Models.User", "User")
                        .WithMany("Registrations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventHub.Infrastructure.Models.Category", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("EventHub.Infrastructure.Models.Event", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Registrations");
                });

            modelBuilder.Entity("EventHub.Infrastructure.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("CreatedEvents");

                    b.Navigation("Registrations");
                });
#pragma warning restore 612, 618
        }
    }
}