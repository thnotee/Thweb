using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thweb.Data.Migrations
{
    /// <inheritdoc />
    public partial class thWebUser_dataAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdDate",
                table: "AspNetUsers");
        }
    }
}
