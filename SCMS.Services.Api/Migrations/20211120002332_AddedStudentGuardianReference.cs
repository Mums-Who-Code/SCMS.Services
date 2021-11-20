// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCMS.Services.Api.Migrations
{
    public partial class AddedStudentGuardianReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_StudentGuardians_Guardians_StudentId",
                table: "StudentGuardians",
                column: "StudentId",
                principalTable: "Guardians",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGuardians_Students_StudentId",
                table: "StudentGuardians",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentGuardians_Guardians_StudentId",
                table: "StudentGuardians");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentGuardians_Students_StudentId",
                table: "StudentGuardians");
        }
    }
}
