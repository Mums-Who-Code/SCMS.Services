// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMCS.Services.Api.Migrations
{
    public partial class StudentSchoolRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StudentSchools_SchoolId",
                table: "StudentSchools",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSchools_StudentId",
                table: "StudentSchools",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSchools_Schools_SchoolId",
                table: "StudentSchools",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSchools_Students_StudentId",
                table: "StudentSchools",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSchools_Schools_SchoolId",
                table: "StudentSchools");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSchools_Students_StudentId",
                table: "StudentSchools");

            migrationBuilder.DropIndex(
                name: "IX_StudentSchools_SchoolId",
                table: "StudentSchools");

            migrationBuilder.DropIndex(
                name: "IX_StudentSchools_StudentId",
                table: "StudentSchools");
        }
    }
}
