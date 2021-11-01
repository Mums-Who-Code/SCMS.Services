// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMCS.Services.Api.Migrations
{
    public partial class StudentSchoolCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSchools",
                table: "StudentSchools");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentSchools");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSchools",
                table: "StudentSchools",
                columns: new[] { "StudentId", "SchoolId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSchools",
                table: "StudentSchools");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "StudentSchools",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSchools",
                table: "StudentSchools",
                column: "Id");
        }
    }
}
