using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoService.Migrations
{
    /// <inheritdoc />
    public partial class partsfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceRecordSpareParts",
                columns: table => new
                {
                    ServiceRecordId = table.Column<int>(type: "int", nullable: false),
                    SparePartId = table.Column<int>(type: "int", nullable: false),
                    QuantityUsed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRecordSpareParts", x => new { x.ServiceRecordId, x.SparePartId });
                    table.ForeignKey(
                        name: "FK_ServiceRecordSpareParts_ServiceRecords_ServiceRecordId",
                        column: x => x.ServiceRecordId,
                        principalTable: "ServiceRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceRecordSpareParts_SpareParts_SparePartId",
                        column: x => x.SparePartId,
                        principalTable: "SpareParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRecordSpareParts_SparePartId",
                table: "ServiceRecordSpareParts",
                column: "SparePartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceRecordSpareParts");
        }
    }
}
