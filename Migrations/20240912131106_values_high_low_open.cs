using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microTrading.Migrations
{
    /// <inheritdoc />
    public partial class values_high_low_open : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "HighValue",
                table: "value_records",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LowValue",
                table: "value_records",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OpenValue",
                table: "value_records",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<float>(
                name: "vol",
                table: "value_records",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighValue",
                table: "value_records");

            migrationBuilder.DropColumn(
                name: "LowValue",
                table: "value_records");

            migrationBuilder.DropColumn(
                name: "OpenValue",
                table: "value_records");

            migrationBuilder.DropColumn(
                name: "vol",
                table: "value_records");
        }
    }
}
