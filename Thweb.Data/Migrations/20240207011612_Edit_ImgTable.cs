using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thweb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Edit_ImgTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Image",
                newName: "SaveFileName");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Image",
                newName: "OriginFileName");

            migrationBuilder.AddColumn<string>(
                name: "DirectoryPath",
                table: "Image",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DirectoryPath",
                table: "Image");

            migrationBuilder.RenameColumn(
                name: "SaveFileName",
                table: "Image",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "OriginFileName",
                table: "Image",
                newName: "FileName");
        }
    }
}
