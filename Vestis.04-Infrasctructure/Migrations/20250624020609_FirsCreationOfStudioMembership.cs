using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vestis._04_Infrasctructure.Migrations
{
    /// <inheritdoc />
    public partial class FirsCreationOfStudioMembership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudioMemberships_Roles_RoleId",
                table: "StudioMemberships");

            migrationBuilder.DropIndex(
                name: "IX_StudioMemberships_RoleId",
                table: "StudioMemberships");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "StudioMemberships");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "StudioMemberships",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "StudioMemberships");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "StudioMemberships",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StudioMemberships_RoleId",
                table: "StudioMemberships",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudioMemberships_Roles_RoleId",
                table: "StudioMemberships",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
