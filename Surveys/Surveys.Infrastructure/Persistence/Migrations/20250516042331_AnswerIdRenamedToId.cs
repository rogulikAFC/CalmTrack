using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surveys.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AnswerIdRenamedToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnswerId",
                table: "Answers",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Answers",
                newName: "AnswerId");
        }
    }
}
