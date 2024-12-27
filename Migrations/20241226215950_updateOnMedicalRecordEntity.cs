using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Vita.Migrations
{
    /// <inheritdoc />
    public partial class updateOnMedicalRecordEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reason_for_visit",
                table: "Medical_Records",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reason_for_visit",
                table: "Medical_Records");
        }
    }
}
