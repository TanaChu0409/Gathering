using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eGathering.Api.Infrastructure
{
    /// <inheritdoc />
    public partial class Updateforgathering : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Gathering",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Gathering_CreatorId",
                table: "Gathering",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gathering_Member_CreatorId",
                table: "Gathering",
                column: "CreatorId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gathering_Member_CreatorId",
                table: "Gathering");

            migrationBuilder.DropIndex(
                name: "IX_Gathering_CreatorId",
                table: "Gathering");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Gathering");
        }
    }
}
