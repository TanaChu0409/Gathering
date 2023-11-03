using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eGathering.Api.Infrastructure
{
    /// <inheritdoc />
    public partial class Updateforaggergateroot1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Invitation_GatheringId",
                table: "Invitation",
                column: "GatheringId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendee_GatheringId",
                table: "Attendee",
                column: "GatheringId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendee_Gathering_GatheringId",
                table: "Attendee",
                column: "GatheringId",
                principalTable: "Gathering",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_Gathering_GatheringId",
                table: "Invitation",
                column: "GatheringId",
                principalTable: "Gathering",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendee_Gathering_GatheringId",
                table: "Attendee");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_Gathering_GatheringId",
                table: "Invitation");

            migrationBuilder.DropIndex(
                name: "IX_Invitation_GatheringId",
                table: "Invitation");

            migrationBuilder.DropIndex(
                name: "IX_Attendee_GatheringId",
                table: "Attendee");
        }
    }
}
