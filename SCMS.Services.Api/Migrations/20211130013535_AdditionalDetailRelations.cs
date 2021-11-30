using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCMS.Services.Api.Migrations
{
    public partial class AdditionalDetailRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddtionalDetails_Students_StudentId",
                table: "AddtionalDetails");

            migrationBuilder.RenameColumn(
                name: "Fide",
                table: "AddtionalDetails",
                newName: "FideId");

            migrationBuilder.CreateIndex(
                name: "IX_AddtionalDetails_CreatedBy",
                table: "AddtionalDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AddtionalDetails_UpdatedBy",
                table: "AddtionalDetails",
                column: "UpdatedBy");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_AddtionalDetails_CreatedBy",
                table: "AddtionalDetails");

            migrationBuilder.DropIndex(
                name: "IX_AddtionalDetails_UpdatedBy",
                table: "AddtionalDetails");

            migrationBuilder.RenameColumn(
                name: "FideId",
                table: "AddtionalDetails",
                newName: "Fide");

            migrationBuilder.AddForeignKey(
                name: "FK_AddtionalDetails_Students_StudentId",
                table: "AddtionalDetails",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
