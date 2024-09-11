using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microTrading.Migrations
{
    /// <inheritdoc />
    public partial class differentvalues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "value",
                table: "value_records");

            migrationBuilder.AddColumn<decimal>(
                name: "CloseValue",
                table: "value_records",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MedianPrice",
                table: "value_records",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OHLCAverage",
                table: "value_records",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TypicalPrice",
                table: "value_records",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WeightedClosePrice",
                table: "value_records",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseValue",
                table: "value_records");

            migrationBuilder.DropColumn(
                name: "MedianPrice",
                table: "value_records");

            migrationBuilder.DropColumn(
                name: "OHLCAverage",
                table: "value_records");

            migrationBuilder.DropColumn(
                name: "TypicalPrice",
                table: "value_records");

            migrationBuilder.DropColumn(
                name: "WeightedClosePrice",
                table: "value_records");

            migrationBuilder.AddColumn<decimal>(
                name: "value",
                table: "value_records",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
