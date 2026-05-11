using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Repository.Migrations
{
    /// <inheritdoc />
    public partial class V26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExist",
                table: "ExamCodes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExist",
                table: "ExamCodes");
        }
    }
}
