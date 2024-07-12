using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microTrading.Migrations
{
    /// <inheritdoc />
    public partial class id_value_generation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actives",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    symbol = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actives", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Run_Perfomances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RunStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    RunStop = table.Column<DateTime>(type: "datetime", nullable: true),
                    idActive = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Run_Perf__3214EC077A0AA797", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Run_Perfomances_ToActives",
                        column: x => x.idActive,
                        principalTable: "Actives",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "value_records",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveId = table.Column<int>(type: "int", nullable: false),
                    record_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    value = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    runId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_value_records", x => x.id);
                    table.ForeignKey(
                        name: "FK_Value_Records_ToRun_Performances",
                        column: x => x.runId,
                        principalTable: "Run_Perfomances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_value_records_ToActives",
                        column: x => x.ActiveId,
                        principalTable: "Actives",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Run_Perfomances_idActive",
                table: "Run_Perfomances",
                column: "idActive");

            migrationBuilder.CreateIndex(
                name: "IX_value_records_runId",
                table: "value_records",
                column: "runId");

            migrationBuilder.CreateIndex(
                name: "Symbol_Index",
                table: "value_records",
                column: "ActiveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "value_records");

            migrationBuilder.DropTable(
                name: "Run_Perfomances");

            migrationBuilder.DropTable(
                name: "Actives");
        }
    }
}
