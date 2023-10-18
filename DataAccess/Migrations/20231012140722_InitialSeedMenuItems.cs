using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeedMenuItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "CreatedBy", "CreatedDate", "Description", "Image", "Name", "Price", "SpecialTag", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Appetizer", "", new DateTime(2023, 10, 12, 16, 7, 22, 40, DateTimeKind.Local).AddTicks(9049), "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://localhost:7052/redmango/spring roll.jpg", "Spring Roll", 7.9900000000000002, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Appetizer", "", new DateTime(2023, 10, 12, 16, 7, 22, 40, DateTimeKind.Local).AddTicks(9114), "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://localhost:7052/redmango/idli.jpg", "Idli", 8.9900000000000002, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Appetizer", "", new DateTime(2023, 10, 12, 16, 7, 22, 40, DateTimeKind.Local).AddTicks(9118), "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://localhost:7052//redmango/pani puri.jpg", "Panu Puri", 8.9900000000000002, "Best Seller", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Entrée", "", new DateTime(2023, 10, 12, 16, 7, 22, 40, DateTimeKind.Local).AddTicks(9118), "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://localhost:7052/redmango/hakka noodles.jpg", "Hakka Noodles", 10.99, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Entrée", "", new DateTime(2023, 10, 12, 16, 7, 22, 40, DateTimeKind.Local).AddTicks(9123), "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://localhost:7052/redmango/malai kofta.jpg", "Malai Kofta", 12.99, "Top Rated", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Entrée", "", new DateTime(2023, 10, 12, 16, 7, 22, 40, DateTimeKind.Local).AddTicks(9128), "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://localhost:7052/redmango/paneer pizza.jpg", "Paneer Pizza", 11.99, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Entrée", "", new DateTime(2023, 10, 12, 16, 7, 22, 40, DateTimeKind.Local).AddTicks(9128), "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://localhost:7052/redmango/paneer tikka.jpg", "Paneer Tikka", 13.99, "Chef's Special", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Dessert", "", new DateTime(2023, 10, 12, 16, 7, 22, 40, DateTimeKind.Local).AddTicks(9132), "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://localhost:7052/redmango/carrot love.jpg", "Carrot Love", 4.9900000000000002, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Dessert", "", new DateTime(2023, 10, 12, 16, 7, 22, 40, DateTimeKind.Local).AddTicks(9137), "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://localhost:7052/redmango/rasmalai.jpg", "Rasmalai", 4.9900000000000002, "Chef's Special", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "Dessert", "", new DateTime(2023, 10, 12, 16, 7, 22, 40, DateTimeKind.Local).AddTicks(9137), "Fusc tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://localhost:7052/redmango/sweet rolls.jpg", "Sweet Rolls", 3.9900000000000002, "Top Rated", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
