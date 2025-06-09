using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sanctions_SanctionTypes_SanctionTypeId",
                table: "Sanctions");

            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SanctionTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "SanctionTypeId",
                table: "Sanctions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Sanctions_SanctionTypes_SanctionTypeId",
                table: "Sanctions",
                column: "SanctionTypeId",
                principalTable: "SanctionTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sanctions_SanctionTypes_SanctionTypeId",
                table: "Sanctions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SanctionTypes");

            migrationBuilder.AlterColumn<int>(
                name: "SanctionTypeId",
                table: "Sanctions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Sanctions_SanctionTypes_SanctionTypeId",
                table: "Sanctions",
                column: "SanctionTypeId",
                principalTable: "SanctionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
