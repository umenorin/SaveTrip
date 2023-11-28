using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaveTrip.Migrations
{
    public partial class MgInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "travels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Destiny = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalCost = table.Column<double>(type: "REAL", nullable: false),
                    TotalPaid = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_travels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Cpf = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "costs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IsPaid = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsCanceled = table.Column<bool>(type: "INTEGER", nullable: false),
                    TotalValor = table.Column<double>(type: "REAL", nullable: false),
                    TotalPaid = table.Column<double>(type: "REAL", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    TravelId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_costs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_costs_travels_TravelId",
                        column: x => x.TravelId,
                        principalTable: "travels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelUser",
                columns: table => new
                {
                    TravelersId = table.Column<int>(type: "INTEGER", nullable: false),
                    TravelsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelUser", x => new { x.TravelersId, x.TravelsId });
                    table.ForeignKey(
                        name: "FK_TravelUser_travels_TravelsId",
                        column: x => x.TravelsId,
                        principalTable: "travels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelUser_users_TravelersId",
                        column: x => x.TravelersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostUser",
                columns: table => new
                {
                    TravelersId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserCostsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostUser", x => new { x.TravelersId, x.UserCostsId });
                    table.ForeignKey(
                        name: "FK_CostUser_costs_UserCostsId",
                        column: x => x.UserCostsId,
                        principalTable: "costs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostUser_users_TravelersId",
                        column: x => x.TravelersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_costs_TravelId",
                table: "costs",
                column: "TravelId");

            migrationBuilder.CreateIndex(
                name: "IX_CostUser_UserCostsId",
                table: "CostUser",
                column: "UserCostsId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelUser_TravelsId",
                table: "TravelUser",
                column: "TravelsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostUser");

            migrationBuilder.DropTable(
                name: "TravelUser");

            migrationBuilder.DropTable(
                name: "costs");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "travels");
        }
    }
}
