using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace zergtool.Migrations
{
    public partial class AddedCtaAdmins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CtaAdmin",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CallToArmsId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CtaAdmin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CtaAdmin_Cta_CallToArmsId",
                        column: x => x.CallToArmsId,
                        principalTable: "Cta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CtaAdmin_CallToArmsId",
                table: "CtaAdmin",
                column: "CallToArmsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CtaAdmin");
        }
    }
}
