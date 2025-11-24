using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class marketuserfirnameandlastnameaddfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Oders_OrderEntityId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderEntityId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderEntityId",
                table: "OrderItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderEntityId",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderEntityId",
                table: "OrderItems",
                column: "OrderEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Oders_OrderEntityId",
                table: "OrderItems",
                column: "OrderEntityId",
                principalTable: "Oders",
                principalColumn: "Id");
        }
    }
}
