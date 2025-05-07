using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurveDentalManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class CreatedTreatmentwithDoctorDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Treatments_TreatmentId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_TreatmentId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "TreatmentId",
                table: "Doctors");

            migrationBuilder.CreateTable(
                name: "DoctorTreatment",
                columns: table => new
                {
                    DoctorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreatmentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorTreatment", x => new { x.DoctorsId, x.TreatmentsId });
                    table.ForeignKey(
                        name: "FK_DoctorTreatment_Doctors_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorTreatment_Treatments_TreatmentsId",
                        column: x => x.TreatmentsId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorTreatment_TreatmentsId",
                table: "DoctorTreatment",
                column: "TreatmentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorTreatment");

            migrationBuilder.AddColumn<Guid>(
                name: "TreatmentId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_TreatmentId",
                table: "Doctors",
                column: "TreatmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Treatments_TreatmentId",
                table: "Doctors",
                column: "TreatmentId",
                principalTable: "Treatments",
                principalColumn: "Id");
        }
    }
}
