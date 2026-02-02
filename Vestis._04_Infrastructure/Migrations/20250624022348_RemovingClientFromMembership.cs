using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vestis._04_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovingClientFromMembership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudioMemberships_Clients_ClientId",
                table: "StudioMemberships");

            migrationBuilder.DropIndex(
                name: "IX_StudioMemberships_ClientId",
                table: "StudioMemberships");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "StudioMemberships");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "StudioMemberships",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StudioMemberships_ClientId",
                table: "StudioMemberships",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudioMemberships_Clients_ClientId",
                table: "StudioMemberships",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
