using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vestis._04_Infrasctructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovingStudioRelationShips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studios_Addresses_AddressId",
                table: "Studios");

            migrationBuilder.DropIndex(
                name: "IX_Studios_AddressId",
                table: "Studios");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Studios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Studios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Studios_AddressId",
                table: "Studios",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Studios_Addresses_AddressId",
                table: "Studios",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
