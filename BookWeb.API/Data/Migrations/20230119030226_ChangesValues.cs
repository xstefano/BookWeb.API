using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookWeb.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangesValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Carts",
                newName: "userName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "userName",
                table: "Carts",
                newName: "UserId");
        }
    }
}
