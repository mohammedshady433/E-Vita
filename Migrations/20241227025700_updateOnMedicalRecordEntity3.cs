using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Vita.Migrations
{
    /// <inheritdoc />
    public partial class updateOnMedicalRecordEntity3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lab",
                table: "Medical_Records",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Radiology",
                table: "Medical_Records",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lab",
                table: "Medical_Records");

            migrationBuilder.DropColumn(
                name: "Radiology",
                table: "Medical_Records");
        }
    }
}
