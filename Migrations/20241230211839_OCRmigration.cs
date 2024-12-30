using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace E_Vita.Migrations
{
    /// <inheritdoc />
    public partial class OCRmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OCRPdfReads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(type: "longtext", nullable: false),
                    FileData = table.Column<byte[]>(type: "longblob", nullable: false),
                    ContentType = table.Column<string>(type: "longtext", nullable: false),
                    ExtractedText = table.Column<string>(type: "longtext", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCRPdfReads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OCRPdfReads_patient_Datas_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patient_Datas",
                        principalColumn: "Patient_ID");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OCRPdfReads_PatientId",
                table: "OCRPdfReads",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OCRPdfReads");
        }
    }
}
