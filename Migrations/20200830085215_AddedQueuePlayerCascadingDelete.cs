using Microsoft.EntityFrameworkCore.Migrations;

namespace zergtool.Migrations
{
    public partial class AddedQueuePlayerCascadingDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueuePlayer_Cta_CallToArmsId",
                table: "QueuePlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_QueueRole_QueuePlayer_QueuePlayerId",
                table: "QueueRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QueuePlayer",
                table: "QueuePlayer");

            migrationBuilder.RenameTable(
                name: "QueuePlayer",
                newName: "QueuePlayers");

            migrationBuilder.RenameIndex(
                name: "IX_QueuePlayer_CallToArmsId",
                table: "QueuePlayers",
                newName: "IX_QueuePlayers_CallToArmsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QueuePlayers",
                table: "QueuePlayers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QueuePlayers_Cta_CallToArmsId",
                table: "QueuePlayers",
                column: "CallToArmsId",
                principalTable: "Cta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QueueRole_QueuePlayers_QueuePlayerId",
                table: "QueueRole",
                column: "QueuePlayerId",
                principalTable: "QueuePlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueuePlayers_Cta_CallToArmsId",
                table: "QueuePlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_QueueRole_QueuePlayers_QueuePlayerId",
                table: "QueueRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QueuePlayers",
                table: "QueuePlayers");

            migrationBuilder.RenameTable(
                name: "QueuePlayers",
                newName: "QueuePlayer");

            migrationBuilder.RenameIndex(
                name: "IX_QueuePlayers_CallToArmsId",
                table: "QueuePlayer",
                newName: "IX_QueuePlayer_CallToArmsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QueuePlayer",
                table: "QueuePlayer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QueuePlayer_Cta_CallToArmsId",
                table: "QueuePlayer",
                column: "CallToArmsId",
                principalTable: "Cta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QueueRole_QueuePlayer_QueuePlayerId",
                table: "QueueRole",
                column: "QueuePlayerId",
                principalTable: "QueuePlayer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
