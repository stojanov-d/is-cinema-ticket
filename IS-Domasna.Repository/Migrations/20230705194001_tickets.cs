using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IS_Domasna.Data.Migrations
{
    public partial class tickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MovieTitle = table.Column<string>(nullable: false),
                    MovieImage = table.Column<string>(nullable: true),
                    MovieDescription = table.Column<string>(nullable: false),
                    MovieAirTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    TicketUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_TicketUserId",
                        column: x => x.TicketUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketUserId",
                table: "Tickets",
                column: "TicketUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
