﻿// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCMS.Services.Api.Migrations
{
    public partial class UpdateGuardianModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Guardians",
                newName: "UpdatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Guardians",
                newName: "UpdateDate");
        }
    }
}