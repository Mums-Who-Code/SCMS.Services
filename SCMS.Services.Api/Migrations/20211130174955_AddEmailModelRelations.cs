// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCMS.Services.Api.Migrations
{
    public partial class AddEmailModelRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Guardians_GuardianId",
                table: "Emails");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Guardians_GuardianId",
                table: "Emails",
                column: "GuardianId",
                principalTable: "Guardians",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Guardians_GuardianId",
                table: "Emails");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Guardians_GuardianId",
                table: "Emails",
                column: "GuardianId",
                principalTable: "Guardians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
