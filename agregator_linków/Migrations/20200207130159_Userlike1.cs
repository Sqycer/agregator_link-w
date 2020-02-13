using Microsoft.EntityFrameworkCore.Migrations;

namespace agregator_linków.Migrations
{
    public partial class Userlike1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "likeid",
                table: "link");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "likeid",
                table: "link",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
