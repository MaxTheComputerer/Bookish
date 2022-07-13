using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookish.Migrations
{
    public partial class NullTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopies_Members_BorrowerId",
                table: "BookCopies");

            migrationBuilder.AlterColumn<int>(
                name: "BorrowerId",
                table: "BookCopies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopies_Members_BorrowerId",
                table: "BookCopies",
                column: "BorrowerId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopies_Members_BorrowerId",
                table: "BookCopies");

            migrationBuilder.AlterColumn<int>(
                name: "BorrowerId",
                table: "BookCopies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopies_Members_BorrowerId",
                table: "BookCopies",
                column: "BorrowerId",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
