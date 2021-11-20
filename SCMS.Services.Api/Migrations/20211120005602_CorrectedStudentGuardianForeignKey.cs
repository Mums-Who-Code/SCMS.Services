// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCMS.Services.Api.Migrations
{
    public partial class CorrectedStudentGuardianForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentGuardians_Guardians_StudentId",
                table: "StudentGuardians");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGuardians_GuardianId",
                table: "StudentGuardians",
                column: "GuardianId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGuardians_Guardians_GuardianId",
                table: "StudentGuardians",
                column: "GuardianId",
                principalTable: "Guardians",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentGuardians_Guardians_GuardianId",
                table: "StudentGuardians");

            migrationBuilder.DropIndex(
                name: "IX_StudentGuardians_GuardianId",
                table: "StudentGuardians");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGuardians_Guardians_StudentId",
                table: "StudentGuardians",
                column: "StudentId",
                principalTable: "Guardians",
                principalColumn: "Id");
        }
    }
}
