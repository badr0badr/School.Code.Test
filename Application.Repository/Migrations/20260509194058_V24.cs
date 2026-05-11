using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Repository.Migrations
{
    /// <inheritdoc />
    public partial class V24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SchoolId2",
                table: "Student",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolId2",
                table: "Student");
        }
    }
}
