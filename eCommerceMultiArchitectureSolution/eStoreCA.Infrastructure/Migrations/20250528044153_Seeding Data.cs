using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eStoreCA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsActive", "LastModifiedAt", "LastModifiedBy", "SoftDeleted", "Title" },
                values: new object[,]
                {
                    { new Guid("a1111111-1111-1111-1111-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Electronics" },
                    { new Guid("a1111111-1111-1111-1111-000000000002"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Clothing" },
                    { new Guid("a1111111-1111-1111-1111-000000000003"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Footwear" },
                    { new Guid("a1111111-1111-1111-1111-000000000004"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Home & Kitchen" },
                    { new Guid("a1111111-1111-1111-1111-000000000005"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Beauty & Personal Care" },
                    { new Guid("a1111111-1111-1111-1111-000000000006"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Books" },
                    { new Guid("a1111111-1111-1111-1111-000000000007"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Health & Wellness" },
                    { new Guid("a1111111-1111-1111-1111-000000000008"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Toys & Games" },
                    { new Guid("a1111111-1111-1111-1111-000000000009"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Jewelry" },
                    { new Guid("a1111111-1111-1111-1111-000000000010"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Watches" },
                    { new Guid("a1111111-1111-1111-1111-000000000011"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Sports & Outdoors" },
                    { new Guid("a1111111-1111-1111-1111-000000000012"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Automotive" },
                    { new Guid("a1111111-1111-1111-1111-000000000013"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Furniture" },
                    { new Guid("a1111111-1111-1111-1111-000000000014"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Office Supplies" },
                    { new Guid("a1111111-1111-1111-1111-000000000015"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Pet Supplies" },
                    { new Guid("a1111111-1111-1111-1111-000000000016"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Baby Products" },
                    { new Guid("a1111111-1111-1111-1111-000000000017"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Garden & Outdoors" },
                    { new Guid("a1111111-1111-1111-1111-000000000018"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Tools & Hardware" },
                    { new Guid("a1111111-1111-1111-1111-000000000019"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Musical Instruments" },
                    { new Guid("a1111111-1111-1111-1111-000000000020"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Groceries" },
                    { new Guid("a1111111-1111-1111-1111-000000000021"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Stationery" },
                    { new Guid("a1111111-1111-1111-1111-000000000022"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Travel Accessories" },
                    { new Guid("a1111111-1111-1111-1111-000000000023"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Bags & Luggage" },
                    { new Guid("a1111111-1111-1111-1111-000000000024"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Mobile Accessories" },
                    { new Guid("a1111111-1111-1111-1111-000000000025"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Camera & Photography" },
                    { new Guid("a1111111-1111-1111-1111-000000000026"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Smart Home Devices" },
                    { new Guid("a1111111-1111-1111-1111-000000000027"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Kitchen Appliances" },
                    { new Guid("a1111111-1111-1111-1111-000000000028"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Cleaning Supplies" },
                    { new Guid("a1111111-1111-1111-1111-000000000029"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Bath Accessories" },
                    { new Guid("a1111111-1111-1111-1111-000000000030"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "DIY & Crafts" },
                    { new Guid("a1111111-1111-1111-1111-000000000031"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Gaming" },
                    { new Guid("a1111111-1111-1111-1111-000000000032"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Nutrition & Supplements" },
                    { new Guid("a1111111-1111-1111-1111-000000000033"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Lighting" },
                    { new Guid("a1111111-1111-1111-1111-000000000034"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Home Decor" },
                    { new Guid("a1111111-1111-1111-1111-000000000035"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Wall Art" },
                    { new Guid("a1111111-1111-1111-1111-000000000036"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Curtains & Blinds" },
                    { new Guid("a1111111-1111-1111-1111-000000000037"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Mattresses & Bedding" },
                    { new Guid("a1111111-1111-1111-1111-000000000038"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Event & Party Supplies" },
                    { new Guid("a1111111-1111-1111-1111-000000000039"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Costumes & Accessories" },
                    { new Guid("a1111111-1111-1111-1111-000000000040"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Seasonal Décor" },
                    { new Guid("a1111111-1111-1111-1111-000000000041"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Green & Eco Products" },
                    { new Guid("a1111111-1111-1111-1111-000000000042"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Industrial Supplies" },
                    { new Guid("a1111111-1111-1111-1111-000000000043"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Safety & Security" },
                    { new Guid("a1111111-1111-1111-1111-000000000044"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Lab & Scientific Supplies" },
                    { new Guid("a1111111-1111-1111-1111-000000000045"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Collectibles" },
                    { new Guid("a1111111-1111-1111-1111-000000000046"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Antiques" },
                    { new Guid("a1111111-1111-1111-1111-000000000047"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Luxury Goods" },
                    { new Guid("a1111111-1111-1111-1111-000000000048"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Digital Products" },
                    { new Guid("a1111111-1111-1111-1111-000000000049"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "Subscription Boxes" },
                    { new Guid("a1111111-1111-1111-1111-000000000050"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), null, null, true, null, null, false, "3D Printing Supplies" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000001"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000002"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000003"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000004"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000005"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000006"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000007"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000008"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000009"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000010"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000011"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000012"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000013"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000014"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000015"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000016"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000017"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000018"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000019"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000020"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000021"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000022"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000023"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000024"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000025"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000026"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000027"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000028"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000029"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000030"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000031"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000032"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000033"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000034"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000035"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000036"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000037"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000038"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000039"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000040"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000041"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000042"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000043"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000044"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000045"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000046"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000047"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000048"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000049"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1111111-1111-1111-1111-000000000050"));
        }
    }
}
