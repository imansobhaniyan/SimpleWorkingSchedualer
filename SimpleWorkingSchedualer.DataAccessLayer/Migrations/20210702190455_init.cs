using Microsoft.EntityFrameworkCore.Migrations;

using SimpleWorkingSchedualer.StorageModels;

namespace SimpleWorkingSchedualer.DataAccessLayer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserName", "Password", "Role" },
                values: new object[] { "admin", "admin", (int)User.UserRole.Admin });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserName", "Password", "Role" },
                values: new object[] { "user", "user", (int)User.UserRole.User });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
