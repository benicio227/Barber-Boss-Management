using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberBossManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NomeDaNovaMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_barberShops_Users_UserId",
                table: "barberShops");

            migrationBuilder.DropForeignKey(
                name: "FK_Revenues_barberShops_BarberShopId",
                table: "Revenues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_barberShops",
                table: "barberShops");

            migrationBuilder.RenameTable(
                name: "barberShops",
                newName: "BarberShops");

            migrationBuilder.RenameIndex(
                name: "IX_barberShops_UserId",
                table: "BarberShops",
                newName: "IX_BarberShops_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarberShops",
                table: "BarberShops",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BarberShops_Users_UserId",
                table: "BarberShops",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Revenues_BarberShops_BarberShopId",
                table: "Revenues",
                column: "BarberShopId",
                principalTable: "BarberShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarberShops_Users_UserId",
                table: "BarberShops");

            migrationBuilder.DropForeignKey(
                name: "FK_Revenues_BarberShops_BarberShopId",
                table: "Revenues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarberShops",
                table: "BarberShops");

            migrationBuilder.RenameTable(
                name: "BarberShops",
                newName: "barberShops");

            migrationBuilder.RenameIndex(
                name: "IX_BarberShops_UserId",
                table: "barberShops",
                newName: "IX_barberShops_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_barberShops",
                table: "barberShops",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_barberShops_Users_UserId",
                table: "barberShops",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Revenues_barberShops_BarberShopId",
                table: "Revenues",
                column: "BarberShopId",
                principalTable: "barberShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
