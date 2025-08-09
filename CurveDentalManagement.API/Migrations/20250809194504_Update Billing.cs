using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurveDentalManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBilling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BillingId",
                table: "Treatments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BillingId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Billings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillingCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_BillingId",
                table: "Treatments",
                column: "BillingId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_BillingId",
                table: "Patients",
                column: "BillingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Billings_BillingId",
                table: "Patients",
                column: "BillingId",
                principalTable: "Billings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Billings_BillingId",
                table: "Treatments",
                column: "BillingId",
                principalTable: "Billings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Billings_BillingId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Billings_BillingId",
                table: "Treatments");

            migrationBuilder.DropTable(
                name: "Billings");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_BillingId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Patients_BillingId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "BillingId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "BillingId",
                table: "Patients");
        }
    }
}
