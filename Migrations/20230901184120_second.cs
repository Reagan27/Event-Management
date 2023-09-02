using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assessment.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Events_EventsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EventsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EventsId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EventsUsers",
                columns: table => new
                {
                    RegisteredEventsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsUsers", x => new { x.RegisteredEventsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_EventsUsers_Events_RegisteredEventsId",
                        column: x => x.RegisteredEventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventsUsers_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventsUsers_UsersId",
                table: "EventsUsers",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventsUsers");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EventsId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EventsId",
                table: "Users",
                column: "EventsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Events_EventsId",
                table: "Users",
                column: "EventsId",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
