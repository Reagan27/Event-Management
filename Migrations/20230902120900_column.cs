using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assessment.Migrations
{
    /// <inheritdoc />
    public partial class column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Events",
                newName: "Slots");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Slots",
                table: "Events",
                newName: "Capacity");
        }
    }
}
