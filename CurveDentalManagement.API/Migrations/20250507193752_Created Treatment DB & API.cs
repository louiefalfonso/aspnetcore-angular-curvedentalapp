using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurveDentalManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class CreatedTreatmentDBAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TreatmentId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreatmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TreatmentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceCoverage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceCoverageAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FollowUpCare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiskBenefits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Indications = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Treatments_TreatmentId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_TreatmentId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "TreatmentId",
                table: "Doctors");
        }
    }
}
