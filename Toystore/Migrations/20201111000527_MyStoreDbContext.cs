using Microsoft.EntityFrameworkCore.Migrations;

namespace Toystore.Migrations
{
    public partial class MyStoreDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    RePassword = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Vendor = table.Column<bool>(nullable: false),
                    Age = table.Column<int>(nullable: true),
                    Gender = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "Category", "Description", "Name", "Photo" },
                values: new object[,]
                {
                    { 1, "Animals", "", "Polar Bear", "" },
                    { 2, "MISC", "", "Piano lesson", "" },
                    { 3, "MISC", "", "Gardening", "" },
                    { 4, "Furnitures", "", "Workbench", "" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Age", "Email", "Gender", "Password", "RePassword", "UserName", "Vendor" },
                values: new object[,]
                {
                    { 1, 30, "aaa@example.com", false, "111", "111", "aaa", true },
                    { 2, 30, "bbb@example.com", false, "111", "111", "bbb", true },
                    { 3, 30, "ccc@example.com", false, "111", "111", "ccc", false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
