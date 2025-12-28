using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Market.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class promotion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TargetUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StartsAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndsAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "Id", "CreatedAtUtc", "EndsAtUtc", "ImageUrl", "IsActive", "IsDeleted", "ModifiedAtUtc", "SortOrder", "StartsAtUtc", "TargetUrl", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 4, 13, 1, 22, 18, 866, DateTimeKind.Local), null, "/images/promotions/winter-sale.jpg", true, false, null, 1, null, "/products?category=zimska-oprema", "Zimska rasprodaja - Do 50% popusta!" },
                    { 2, new DateTime(2022, 4, 13, 1, 22, 18, 866, DateTimeKind.Local), null, "/images/promotions/new-arrivals.jpg", true, false, null, 2, null, "/products?sort=newest", "Novi proizvodi - Pogledajte kolekciju" },
                    { 3, new DateTime(2022, 4, 13, 1, 22, 18, 866, DateTimeKind.Local), null, "/images/promotions/free-shipping.jpg", true, false, null, 3, null, null, "Besplatna dostava za narudžbe preko 50 BAM" },
                    { 4, new DateTime(2022, 4, 13, 1, 22, 18, 866, DateTimeKind.Local), null, "/images/promotions/brand-sale.jpg", true, false, null, 4, null, "/products?brand=premium", "Akcija - Brendirana oprema -30%" },
                    { 5, new DateTime(2022, 4, 13, 1, 22, 18, 866, DateTimeKind.Local), null, "/images/promotions/loyalty.jpg", true, false, null, 5, null, "/loyalty", "Loyalty program - Sakupljajte bodove" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_IsActive_SortOrder",
                table: "Promotions",
                columns: new[] { "IsActive", "SortOrder" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promotions");
        }
    }
}
