using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KCK_Elektroniczny_Dziennik_Szkolny.Migrations
{
    public partial class ulcix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_SupervisingTeacherId",
                table: "Classes");

            migrationBuilder.AlterColumn<int>(
                name: "SupervisingTeacherId",
                table: "Classes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_SupervisingTeacherId",
                table: "Classes",
                column: "SupervisingTeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_SupervisingTeacherId",
                table: "Classes");

            migrationBuilder.AlterColumn<int>(
                name: "SupervisingTeacherId",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_SupervisingTeacherId",
                table: "Classes",
                column: "SupervisingTeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
