using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace zergtool.Migrations
{
    public partial class ChangedCtaPlayerParty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Cta_CallToArmsId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_CallToArmsId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "CallToArmsId",
                table: "Player");

            migrationBuilder.AddColumn<Guid>(
                name: "PartyId",
                table: "Player",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Party",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CallToArmsId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Party", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Party_Cta_CallToArmsId",
                        column: x => x.CallToArmsId,
                        principalTable: "Cta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_PartyId",
                table: "Player",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Party_CallToArmsId",
                table: "Party",
                column: "CallToArmsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Party_PartyId",
                table: "Player",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Party_PartyId",
                table: "Player");

            migrationBuilder.DropTable(
                name: "Party");

            migrationBuilder.DropIndex(
                name: "IX_Player_PartyId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "Player");

            migrationBuilder.AddColumn<Guid>(
                name: "CallToArmsId",
                table: "Player",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_CallToArmsId",
                table: "Player",
                column: "CallToArmsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Cta_CallToArmsId",
                table: "Player",
                column: "CallToArmsId",
                principalTable: "Cta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
