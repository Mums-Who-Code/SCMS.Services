// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCMS.Services.Api.Migrations
{
    public partial class UpdatedStudentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalDetails_Students_StudentId",
                table: "AdditionalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSchools_Schools_SchoolId",
                table: "StudentSchools");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSchools_Students_StudentId",
                table: "StudentSchools");

            migrationBuilder.DropIndex(
                name: "IX_StudentSchools_StudentId",
                table: "StudentSchools");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalDetails_StudentId",
                table: "AdditionalDetails");

            migrationBuilder.AddColumn<string>(
                name: "FideId",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Students_SchoolId",
                table: "Students",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalDetails_StudentId",
                table: "AdditionalDetails",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalDetails_Students_StudentId",
                table: "AdditionalDetails",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Schools_SchoolId",
                table: "Students",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSchools_Schools_SchoolId",
                table: "StudentSchools",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSchools_Students_StudentId",
                table: "StudentSchools",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalDetails_Students_StudentId",
                table: "AdditionalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Schools_SchoolId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSchools_Schools_SchoolId",
                table: "StudentSchools");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSchools_Students_StudentId",
                table: "StudentSchools");

            migrationBuilder.DropIndex(
                name: "IX_Students_SchoolId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalDetails_StudentId",
                table: "AdditionalDetails");

            migrationBuilder.DropColumn(
                name: "FideId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Students");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSchools_StudentId",
                table: "StudentSchools",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalDetails_StudentId",
                table: "AdditionalDetails",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalDetails_Students_StudentId",
                table: "AdditionalDetails",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

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
    }
}
