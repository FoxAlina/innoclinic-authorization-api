using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoclinicAutho.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class renameuserfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DateUpdated",
                table: "Users",
                newName: "UpdateDateTime");

            migrationBuilder.RenameColumn(
                name: "DateDeleted",
                table: "Users",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Users",
                newName: "CreateDateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "UpdateDateTime",
                table: "Users",
                newName: "DateUpdated");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "Users",
                newName: "DateDeleted");

            migrationBuilder.RenameColumn(
                name: "CreateDateTime",
                table: "Users",
                newName: "DateCreated");
        }
    }
}
