using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoService.Migrations
{
    /// <inheritdoc />
    public partial class partsfix5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ServiceRecordSpareParts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ServiceRecordSpareParts");
        }
    }
}
