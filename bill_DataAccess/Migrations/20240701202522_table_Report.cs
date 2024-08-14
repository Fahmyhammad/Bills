using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bill_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class table_Report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_salesInvoices_SalesId",
                        column: x => x.SalesId,
                        principalTable: "salesInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_SalesId",
                table: "Reports",
                column: "SalesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

        }
    }
}
