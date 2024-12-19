using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Vita.Migrations
{
    /// <inheritdoc />
    public partial class reset_pass_modified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reset_Pass_Logs_Doctors_User_Name",
                table: "Reset_Pass_Logs");

            migrationBuilder.DropIndex(
                name: "IX_Reset_Pass_Logs_User_Name",
                table: "Reset_Pass_Logs");

            migrationBuilder.AlterColumn<string>(
                name: "User_Name",
                table: "Reset_Pass_Logs",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Doc_ID",
                table: "Reset_Pass_Logs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reset_Pass_Logs_Doc_ID",
                table: "Reset_Pass_Logs",
                column: "Doc_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reset_Pass_Logs_Doctors_Doc_ID",
                table: "Reset_Pass_Logs",
                column: "Doc_ID",
                principalTable: "Doctors",
                principalColumn: "Doctor_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reset_Pass_Logs_Doctors_Doc_ID",
                table: "Reset_Pass_Logs");

            migrationBuilder.DropIndex(
                name: "IX_Reset_Pass_Logs_Doc_ID",
                table: "Reset_Pass_Logs");

            migrationBuilder.DropColumn(
                name: "Doc_ID",
                table: "Reset_Pass_Logs");

            migrationBuilder.AlterColumn<int>(
                name: "User_Name",
                table: "Reset_Pass_Logs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.CreateIndex(
                name: "IX_Reset_Pass_Logs_User_Name",
                table: "Reset_Pass_Logs",
                column: "User_Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Reset_Pass_Logs_Doctors_User_Name",
                table: "Reset_Pass_Logs",
                column: "User_Name",
                principalTable: "Doctors",
                principalColumn: "Doctor_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
