using Microsoft.EntityFrameworkCore.Migrations;

namespace Dating.Migrations
{
    public partial class photos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Photos",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "Photos",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<int>(
                name: "Bytes",
                table: "Photos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Photos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResourceType",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecureUrl",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Photos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Photos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bytes",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Format",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ResourceType",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "SecureUrl",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Signature",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Photos",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Photos",
                newName: "DateAdded");
        }
    }
}
