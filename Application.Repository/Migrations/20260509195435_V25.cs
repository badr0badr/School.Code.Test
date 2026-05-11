using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Repository.Migrations
{
    /// <inheritdoc />
    public partial class V25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolId2",
                table: "Student");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SchoolId2",
                table: "Student",
                type: "bigint",
                nullable: true);
        }
    }
}
