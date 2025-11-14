using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vestis._04_Infrasctructure.Migrations
{
    /// <inheritdoc />
    public partial class addingProfileImmageToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImg",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImg",
                table: "Users");
        }
    }
}
