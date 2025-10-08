using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _1121726Final.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TableGroups1121726",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableGroups1121726", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "TableConcerts1121726",
                columns: table => new
                {
                    ConcertId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    ConcertName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableConcerts1121726", x => x.ConcertId);
                    table.ForeignKey(
                        name: "FK_TableConcerts1121726_TableGroups1121726_GroupId",
                        column: x => x.GroupId,
                        principalTable: "TableGroups1121726",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TableTickets1121726",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcertId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPurchased = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableTickets1121726", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_TableTickets1121726_TableConcerts1121726_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "TableConcerts1121726",
                        principalColumn: "ConcertId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableConcerts1121726_GroupId",
                table: "TableConcerts1121726",
                column: "GroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TableTickets1121726_ConcertId",
                table: "TableTickets1121726",
                column: "ConcertId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableTickets1121726");

            migrationBuilder.DropTable(
                name: "TableConcerts1121726");

            migrationBuilder.DropTable(
                name: "TableGroups1121726");
        }
    }
}
