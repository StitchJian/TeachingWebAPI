using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grade_sheet_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    number = table.Column<int>(type: "int", nullable: false, comment: "流水號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "姓名"),
                    grade = table.Column<int>(type: "int", nullable: false, comment: "成績")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.number);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grades");
        }
    }
}
