using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace zergtool.Migrations
{
    public partial class AddedCtaDeleteCascading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CtaAdmin_Cta_CallToArmsId",
                table: "CtaAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_Party_Cta_CallToArmsId",
                table: "Party");

            migrationBuilder.DropForeignKey(
                name: "FK_QueuePlayers_Cta_CallToArmsId",
                table: "QueuePlayers");

            migrationBuilder.DropIndex(
                name: "IX_QueuePlayers_CallToArmsId",
                table: "QueuePlayers");

            migrationBuilder.DropIndex(
                name: "IX_Party_CallToArmsId",
                table: "Party");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cta",
                table: "Cta");

            migrationBuilder.DropColumn(
                name: "CallToArmsId",
                table: "QueuePlayers");

            migrationBuilder.DropColumn(
                name: "CallToArmsId",
                table: "Party");

            migrationBuilder.RenameTable(
                name: "Cta",
                newName: "Ctas");

            migrationBuilder.AddColumn<Guid>(
                name: "CtaId",
                table: "QueuePlayers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CtaId",
                table: "Party",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ctas",
                table: "Ctas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QueuePlayers_CtaId",
                table: "QueuePlayers",
                column: "CtaId");

            migrationBuilder.CreateIndex(
                name: "IX_Party_CtaId",
                table: "Party",
                column: "CtaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CtaAdmin_Ctas_CallToArmsId",
                table: "CtaAdmin",
                column: "CallToArmsId",
                principalTable: "Ctas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Party_Ctas_CtaId",
                table: "Party",
                column: "CtaId",
                principalTable: "Ctas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QueuePlayers_Ctas_CtaId",
                table: "QueuePlayers",
                column: "CtaId",
                principalTable: "Ctas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CtaAdmin_Ctas_CallToArmsId",
                table: "CtaAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_Party_Ctas_CtaId",
                table: "Party");

            migrationBuilder.DropForeignKey(
                name: "FK_QueuePlayers_Ctas_CtaId",
                table: "QueuePlayers");

            migrationBuilder.DropIndex(
                name: "IX_QueuePlayers_CtaId",
                table: "QueuePlayers");

            migrationBuilder.DropIndex(
                name: "IX_Party_CtaId",
                table: "Party");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ctas",
                table: "Ctas");

            migrationBuilder.DropColumn(
                name: "CtaId",
                table: "QueuePlayers");

            migrationBuilder.DropColumn(
                name: "CtaId",
                table: "Party");

            migrationBuilder.RenameTable(
                name: "Ctas",
                newName: "Cta");

            migrationBuilder.AddColumn<Guid>(
                name: "CallToArmsId",
                table: "QueuePlayers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CallToArmsId",
                table: "Party",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cta",
                table: "Cta",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QueuePlayers_CallToArmsId",
                table: "QueuePlayers",
                column: "CallToArmsId");

            migrationBuilder.CreateIndex(
                name: "IX_Party_CallToArmsId",
                table: "Party",
                column: "CallToArmsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CtaAdmin_Cta_CallToArmsId",
                table: "CtaAdmin",
                column: "CallToArmsId",
                principalTable: "Cta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Party_Cta_CallToArmsId",
                table: "Party",
                column: "CallToArmsId",
                principalTable: "Cta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QueuePlayers_Cta_CallToArmsId",
                table: "QueuePlayers",
                column: "CallToArmsId",
                principalTable: "Cta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
