using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eGathering.Api.Infrastructure
{
    /// <inheritdoc />
    public partial class Updateforvalueobject3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Member_LastName",
                table: "Member");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Member_LastName",
                table: "Member",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
