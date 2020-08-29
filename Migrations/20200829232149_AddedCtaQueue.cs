using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace zergtool.Migrations
{
    public partial class AddedCtaQueue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QueuePlayer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CallToArmsId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueuePlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueuePlayer_Cta_CallToArmsId",
                        column: x => x.CallToArmsId,
                        principalTable: "Cta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QueueRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: true),
                    QueuePlayerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueueRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueueRole_QueuePlayer_QueuePlayerId",
                        column: x => x.QueuePlayerId,
                        principalTable: "QueuePlayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QueueRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QueuePlayer_CallToArmsId",
                table: "QueuePlayer",
                column: "CallToArmsId");

            migrationBuilder.CreateIndex(
                name: "IX_QueueRole_QueuePlayerId",
                table: "QueueRole",
                column: "QueuePlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_QueueRole_RoleId",
                table: "QueueRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueueRole");

            migrationBuilder.DropTable(
                name: "QueuePlayer");
        }
    }
}
