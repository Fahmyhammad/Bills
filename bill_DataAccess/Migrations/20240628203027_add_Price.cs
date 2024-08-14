using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bill_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_Price : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "salesInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "salesInvoices");
        }
    }
}
