using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KCK_Elektroniczny_Dziennik_Szkolny.Migrations
{
    /// <inheritdoc />
    public partial class @class : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Classes");
        }
    }
}
