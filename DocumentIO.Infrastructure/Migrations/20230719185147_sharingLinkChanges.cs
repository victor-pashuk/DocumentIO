using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentIO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sharingLinkChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "SharingLinks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "SharingLinks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
