using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Events");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Technology" },
                    { 2, "Music" },
                    { 3, "Art" },
                    { 4, "Sports" },
                    { 5, "Education" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc", new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@example.com", "John", "Doe", "AQAAAAIAAYagAAAAEHJL24YHOSxiq4VLglJpxUvkAFDR785wLZCWxyHO4qEOB5vjfD7lqMomVfqXm0ziXQ==", "john_doe" },
                    { "52569acc-8c9f-4e79-86ef-d55071e0be3d", new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@example.com", "Jane", "Smith", "AQAAAAIAAYagAAAAENkjlyvjVLiWCmUCQYswkHhjKGVRkZoqzJmLdzHRVCjdGXxNVqAHTQc0AaxkxBJVMA==", "jane_smith" },
                    { "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40", new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "mike.jones@example.com", "Mike", "Jones", "AQAAAAIAAYagAAAAEGoOfPaPkYDr8E2NlcVSz0tvfOpE8461ckK+cbeDeCT+otUYz8pMaAQ1OaEndom8Uw==", "mike_jones" },
                    { "92b2c75a-ab33-425a-be05-6d6f7af74a5a", new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "lucy.brown@example.com", "Lucy", "Brown", "AQAAAAIAAYagAAAAELrFIh5XRSZpO61TGmp6yE3hoQzj9p1SRE4SvMIEC53rdJghrOUGjRW5k1CNGWgCGA==", "lucy_brown" },
                    { "f593b51d-ef90-4914-9099-3188302616dc", new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "susan.lee@example.com", "Susan", "Lee", "AQAAAAIAAYagAAAAEAoeQFglNWH45aAsqOg6q6/DozYW9jaWuxOUYD3Nhxu757GaLAbOYSTrEWh1oUlUSQ==", "susan_lee" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CategoryId", "CreatorId", "Date", "Description", "Location", "Title" },
                values: new object[,]
                {
                    { 1, 1, "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc", new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "A conference on the latest in technology", "Los Angeles, CA", "Tech Conference" },
                    { 2, 2, "52569acc-8c9f-4e79-86ef-d55071e0be3d", new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Live rock music concert", "New York, NY", "Rock Concert" },
                    { 3, 3, "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40", new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "New art exhibition opening", "Paris, France", "Art Gallery Opening" },
                    { 4, 4, "92b2c75a-ab33-425a-be05-6d6f7af74a5a", new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Exciting football competition", "London, UK", "Football Tournament" },
                    { 5, 5, "52569acc-8c9f-4e79-86ef-d55071e0be3d", new DateTime(2024, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "A webinar about online learning", "Virtual", "Online Education Webinar" },
                    { 6, 1, "f593b51d-ef90-4914-9099-3188302616dc", new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seminar on AI and machine learning", "San Francisco, CA", "AI and Machine Learning Seminar" },
                    { 7, 2, "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40", new DateTime(2024, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjoy a night of live jazz music", "Chicago, IL", "Jazz Music Night" },
                    { 8, 3, "92b2c75a-ab33-425a-be05-6d6f7af74a5a", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Explore the world of modern art", "Berlin, Germany", "Modern Art Exhibition" },
                    { 9, 4, "f593b51d-ef90-4914-9099-3188302616dc", new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Annual basketball competition", "Los Angeles, CA", "Basketball Championship" },
                    { 10, 5, "52569acc-8c9f-4e79-86ef-d55071e0be3d", new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intensive coding bootcamp", "Austin, TX", "Coding Bootcamp" },
                    { 11, 1, "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40", new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Workshop for web development", "Online", "Web Development Workshop" },
                    { 12, 2, "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc", new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Exciting pop concert", "Miami, FL", "Pop Concert" },
                    { 13, 3, "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc", new DateTime(2024, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Workshop on digital art techniques", "Amsterdam, Netherlands", "Digital Art Workshop" },
                    { 14, 4, "52569acc-8c9f-4e79-86ef-d55071e0be3d", new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Relax and rejuvenate with yoga", "Bali, Indonesia", "Yoga Retreat" },
                    { 15, 5, "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Explore the world of online learning", "Virtual", "Online Learning Expo" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreatedAt", "EventId", "UserId" },
                values: new object[,]
                {
                    { 1, "Great event!", new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc" },
                    { 2, "Looking forward to it!", new DateTime(2024, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "52569acc-8c9f-4e79-86ef-d55071e0be3d" },
                    { 3, "This sounds amazing!", new DateTime(2024, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40" },
                    { 4, "I am going to join this!", new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "92b2c75a-ab33-425a-be05-6d6f7af74a5a" },
                    { 5, "Excited to attend!", new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "f593b51d-ef90-4914-9099-3188302616dc" }
                });

            migrationBuilder.InsertData(
                table: "EventRegistrations",
                columns: new[] { "Id", "EventId", "RegisteredAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc" },
                    { 2, 2, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "52569acc-8c9f-4e79-86ef-d55071e0be3d" },
                    { 3, 3, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40" },
                    { 4, 4, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "92b2c75a-ab33-425a-be05-6d6f7af74a5a" },
                    { 5, 5, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "f593b51d-ef90-4914-9099-3188302616dc" },
                    { 6, 6, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc" },
                    { 7, 7, new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "52569acc-8c9f-4e79-86ef-d55071e0be3d" },
                    { 8, 8, new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40" },
                    { 9, 9, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "92b2c75a-ab33-425a-be05-6d6f7af74a5a" },
                    { 10, 10, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "f593b51d-ef90-4914-9099-3188302616dc" },
                    { 11, 11, new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc" },
                    { 12, 12, new DateTime(2024, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "52569acc-8c9f-4e79-86ef-d55071e0be3d" },
                    { 13, 13, new DateTime(2024, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40" },
                    { 14, 14, new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "92b2c75a-ab33-425a-be05-6d6f7af74a5a" },
                    { 15, 15, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "f593b51d-ef90-4914-9099-3188302616dc" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EventRegistrations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EventRegistrations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventRegistrations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventRegistrations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EventRegistrations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EventRegistrations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EventRegistrations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EventRegistrations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EventRegistrations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EventRegistrations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "EventRegistrations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "EventRegistrations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "EventRegistrations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "EventRegistrations",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "EventRegistrations",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3f8cece7-c485-4d8c-95b2-bfd3d97d28cc");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "52569acc-8c9f-4e79-86ef-d55071e0be3d");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "8fc80a8c-5fd1-4745-bcb4-6b3b00cf9f40");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "92b2c75a-ab33-425a-be05-6d6f7af74a5a");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "f593b51d-ef90-4914-9099-3188302616dc");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Event's image link");
        }
    }
}
