using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoService.Migrations
{
    /// <inheritdoc />
    public partial class partsfix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "SpareParts");

            migrationBuilder.RenameColumn(
                name: "QuantityUsed",
                table: "ServiceRecordSpareParts",
                newName: "Quantity");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ServiceRecordSpareParts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ServiceRecordSpareParts");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ServiceRecordSpareParts",
                newName: "QuantityUsed");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "SpareParts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
