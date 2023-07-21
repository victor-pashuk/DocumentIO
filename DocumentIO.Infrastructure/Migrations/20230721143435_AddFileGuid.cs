using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFileGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileURL",
                table: "Documents",
                newName: "FileGuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileGuid",
                table: "Documents",
                newName: "FileURL");
        }
    }
}
