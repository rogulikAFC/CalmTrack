using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surveys.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FormInstanceResultStoringAddedAndSurveysArchivingAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Scales",
                table: "Scales");

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Surveys",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Scales",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "FormInstances",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ResultId",
                table: "FormInstances",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scales",
                table: "Scales",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Scales_SurveyId_Value",
                table: "Scales",
                columns: new[] { "SurveyId", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormInstances_ResultId",
                table: "FormInstances",
                column: "ResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormInstances_Scales_ResultId",
                table: "FormInstances",
                column: "ResultId",
                principalTable: "Scales",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormInstances_Scales_ResultId",
                table: "FormInstances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scales",
                table: "Scales");

            migrationBuilder.DropIndex(
                name: "IX_Scales_SurveyId_Value",
                table: "Scales");

            migrationBuilder.DropIndex(
                name: "IX_FormInstances_ResultId",
                table: "FormInstances");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Scales");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "FormInstances");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "FormInstances");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scales",
                table: "Scales",
                columns: new[] { "SurveyId", "Value" });
        }
    }
}
