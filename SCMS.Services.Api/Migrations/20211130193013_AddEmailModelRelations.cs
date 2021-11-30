// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCMS.Services.Api.Migrations
{
    public partial class AddEmailModelRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddtionalDetails_Students_StudentId",
                table: "AddtionalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AddtionalDetails_Users_CreatedBy",
                table: "AddtionalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AddtionalDetails_Users_UpdatedBy",
                table: "AddtionalDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddtionalDetails",
                table: "AddtionalDetails");

            migrationBuilder.RenameTable(
                name: "AddtionalDetails",
                newName: "AdditionalDetails");

            migrationBuilder.RenameIndex(
                name: "IX_AddtionalDetails_UpdatedBy",
                table: "AdditionalDetails",
                newName: "IX_AdditionalDetails_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_AddtionalDetails_StudentId",
                table: "AdditionalDetails",
                newName: "IX_AdditionalDetails_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_AddtionalDetails_CreatedBy",
                table: "AdditionalDetails",
                newName: "IX_AdditionalDetails_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdditionalDetails",
                table: "AdditionalDetails",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    GuardianId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emails_Guardians_GuardianId",
                        column: x => x.GuardianId,
                        principalTable: "Guardians",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Emails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Emails_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emails_CreatedBy",
                table: "Emails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_GuardianId",
                table: "Emails",
                column: "GuardianId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emails_UpdatedBy",
                table: "Emails",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalDetails_Students_StudentId",
                table: "AdditionalDetails",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalDetails_Users_CreatedBy",
                table: "AdditionalDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalDetails_Users_UpdatedBy",
                table: "AdditionalDetails",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalDetails_Students_StudentId",
                table: "AdditionalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalDetails_Users_CreatedBy",
                table: "AdditionalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalDetails_Users_UpdatedBy",
                table: "AdditionalDetails");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdditionalDetails",
                table: "AdditionalDetails");

            migrationBuilder.RenameTable(
                name: "AdditionalDetails",
                newName: "AddtionalDetails");

            migrationBuilder.RenameIndex(
                name: "IX_AdditionalDetails_UpdatedBy",
                table: "AddtionalDetails",
                newName: "IX_AddtionalDetails_UpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_AdditionalDetails_StudentId",
                table: "AddtionalDetails",
                newName: "IX_AddtionalDetails_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_AdditionalDetails_CreatedBy",
                table: "AddtionalDetails",
                newName: "IX_AddtionalDetails_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddtionalDetails",
                table: "AddtionalDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AddtionalDetails_Students_StudentId",
                table: "AddtionalDetails",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AddtionalDetails_Users_CreatedBy",
                table: "AddtionalDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AddtionalDetails_Users_UpdatedBy",
                table: "AddtionalDetails",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
