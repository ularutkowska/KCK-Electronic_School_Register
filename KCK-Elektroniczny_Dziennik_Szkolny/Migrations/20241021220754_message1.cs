using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KCK_Elektroniczny_Dziennik_Szkolny.Migrations
{
    /// <inheritdoc />
    public partial class message1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiverRole",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderRole",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverRole",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderRole",
                table: "Messages");
        }
    }
}
