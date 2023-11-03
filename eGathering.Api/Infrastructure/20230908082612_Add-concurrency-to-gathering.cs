using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eGathering.Api.Infrastructure
{
    /// <inheritdoc />
    public partial class Addconcurrencytogathering : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Gathering",
                type: "bigint",
                rowVersion: true,
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Gathering");
        }
    }
}
