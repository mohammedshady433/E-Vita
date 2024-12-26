using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace E_Vita.Migrations
{
    /// <inheritdoc />
    public partial class newdatabase_for_evita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Doctor_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Speciality = table.Column<string>(type: "longtext", nullable: false),
                    User_Name = table.Column<string>(type: "longtext", nullable: false),
                    Pass = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Doctor_ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "patient_Datas",
                columns: table => new
                {
                    Patient_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    contact = table.Column<string>(type: "longtext", nullable: false),
                    diseases = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    Nationality = table.Column<string>(type: "longtext", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Birth_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patient_Datas", x => x.Patient_ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Nurses",
                columns: table => new
                {
                    Nurse_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Doctor_ID = table.Column<int>(type: "int", nullable: false),
                    user_name = table.Column<string>(type: "longtext", nullable: false),
                    password = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurses", x => x.Nurse_ID);
                    table.ForeignKey(
                        name: "FK_Nurses_Doctors_Doctor_ID",
                        column: x => x.Doctor_ID,
                        principalTable: "Doctors",
                        principalColumn: "Doctor_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reset_Pass_Logs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    New_Pass = table.Column<string>(type: "longtext", nullable: false),
                    Admin_Pass = table.Column<string>(type: "longtext", nullable: false),
                    User_Name = table.Column<string>(type: "longtext", nullable: false),
                    Doc_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reset_Pass_Logs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reset_Pass_Logs_Doctors_Doc_ID",
                        column: x => x.Doc_ID,
                        principalTable: "Doctors",
                        principalColumn: "Doctor_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Appointments_DB",
                columns: table => new
                {
                    Appointment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Time = table.Column<string>(type: "longtext", nullable: false),
                    Patient_ID = table.Column<int>(type: "int", nullable: false),
                    Doctor_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments_DB", x => x.Appointment_ID);
                    table.ForeignKey(
                        name: "FK_Appointments_DB_Doctors_Doctor_ID",
                        column: x => x.Doctor_ID,
                        principalTable: "Doctors",
                        principalColumn: "Doctor_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_DB_patient_Datas_Patient_ID",
                        column: x => x.Patient_ID,
                        principalTable: "patient_Datas",
                        principalColumn: "Patient_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Medical_Records",
                columns: table => new
                {
                    Record_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Future_Plan = table.Column<string>(type: "longtext", nullable: false),
                    Disease = table.Column<string>(type: "longtext", nullable: false),
                    Medication = table.Column<string>(type: "longtext", nullable: false),
                    Surgery = table.Column<string>(type: "longtext", nullable: false),
                    Family_History = table.Column<string>(type: "longtext", nullable: false),
                    Patient_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medical_Records", x => x.Record_ID);
                    table.ForeignKey(
                        name: "FK_Medical_Records_patient_Datas_Patient_ID",
                        column: x => x.Patient_ID,
                        principalTable: "patient_Datas",
                        principalColumn: "Patient_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Prescription_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Dosage = table.Column<int>(type: "int", nullable: false),
                    Patient_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Prescription_ID);
                    table.ForeignKey(
                        name: "FK_Prescriptions_patient_Datas_Patient_ID",
                        column: x => x.Patient_ID,
                        principalTable: "patient_Datas",
                        principalColumn: "Patient_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "patient_Doctor_Nurses",
                columns: table => new
                {
                    Nurse_ID = table.Column<int>(type: "int", nullable: false),
                    Patient_ID = table.Column<int>(type: "int", nullable: false),
                    Doctor_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patient_Doctor_Nurses", x => new { x.Nurse_ID, x.Patient_ID, x.Doctor_ID });
                    table.ForeignKey(
                        name: "FK_patient_Doctor_Nurses_Doctors_Doctor_ID",
                        column: x => x.Doctor_ID,
                        principalTable: "Doctors",
                        principalColumn: "Doctor_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patient_Doctor_Nurses_Nurses_Nurse_ID",
                        column: x => x.Nurse_ID,
                        principalTable: "Nurses",
                        principalColumn: "Nurse_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patient_Doctor_Nurses_patient_Datas_Patient_ID",
                        column: x => x.Patient_ID,
                        principalTable: "patient_Datas",
                        principalColumn: "Patient_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DB_Doctor_ID",
                table: "Appointments_DB",
                column: "Doctor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DB_Patient_ID",
                table: "Appointments_DB",
                column: "Patient_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Records_Patient_ID",
                table: "Medical_Records",
                column: "Patient_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_Doctor_ID",
                table: "Nurses",
                column: "Doctor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_patient_Doctor_Nurses_Doctor_ID",
                table: "patient_Doctor_Nurses",
                column: "Doctor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_patient_Doctor_Nurses_Patient_ID",
                table: "patient_Doctor_Nurses",
                column: "Patient_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_Patient_ID",
                table: "Prescriptions",
                column: "Patient_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Reset_Pass_Logs_Doc_ID",
                table: "Reset_Pass_Logs",
                column: "Doc_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments_DB");

            migrationBuilder.DropTable(
                name: "Medical_Records");

            migrationBuilder.DropTable(
                name: "patient_Doctor_Nurses");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Reset_Pass_Logs");

            migrationBuilder.DropTable(
                name: "Nurses");

            migrationBuilder.DropTable(
                name: "patient_Datas");

            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
