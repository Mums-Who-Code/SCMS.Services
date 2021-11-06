// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCMS.Services.Api.Migrations
{
    public partial class StudentSchoolModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentSchools",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSchools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentSchools_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSchools_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentSchools_CreatedBy",
                table: "StudentSchools",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSchools_UpdatedBy",
                table: "StudentSchools",
                column: "UpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentSchools");
        }
    }
}
