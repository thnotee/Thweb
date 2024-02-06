using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thweb.Data.Migrations
{
    /// <inheritdoc />
    public partial class modifyProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductTF",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductTF",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
