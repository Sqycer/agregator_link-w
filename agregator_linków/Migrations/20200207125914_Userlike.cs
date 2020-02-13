using Microsoft.EntityFrameworkCore.Migrations;

namespace agregator_linków.Migrations
{
    public partial class Userlike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_link_users_userid",
                table: "link");

            migrationBuilder.DropForeignKey(
                name: "FK_userLikes_likes_likeid",
                table: "userLikes");

            migrationBuilder.DropTable(
                name: "likes");

            migrationBuilder.DropIndex(
                name: "IX_userLikes_likeid",
                table: "userLikes");

            migrationBuilder.DropIndex(
                name: "IX_link_userid",
                table: "link");

            migrationBuilder.DropColumn(
                name: "likeid",
                table: "userLikes");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "link");

            migrationBuilder.AddColumn<int>(
                name: "linkid",
                table: "userLikes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "like",
                table: "link",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ownerid",
                table: "link",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_userLikes_linkid",
                table: "userLikes",
                column: "linkid");

            migrationBuilder.CreateIndex(
                name: "IX_link_ownerid",
                table: "link",
                column: "ownerid");

            migrationBuilder.AddForeignKey(
                name: "FK_link_users_ownerid",
                table: "link",
                column: "ownerid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userLikes_link_linkid",
                table: "userLikes",
                column: "linkid",
                principalTable: "link",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_link_users_ownerid",
                table: "link");

            migrationBuilder.DropForeignKey(
                name: "FK_userLikes_link_linkid",
                table: "userLikes");

            migrationBuilder.DropIndex(
                name: "IX_userLikes_linkid",
                table: "userLikes");

            migrationBuilder.DropIndex(
                name: "IX_link_ownerid",
                table: "link");

            migrationBuilder.DropColumn(
                name: "linkid",
                table: "userLikes");

            migrationBuilder.DropColumn(
                name: "like",
                table: "link");

            migrationBuilder.DropColumn(
                name: "ownerid",
                table: "link");

            migrationBuilder.AddColumn<int>(
                name: "likeid",
                table: "userLikes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "link",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "likes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    like = table.Column<int>(type: "int", nullable: false),
                    linkid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_likes", x => x.id);
                    table.ForeignKey(
                        name: "FK_likes_link_linkid",
                        column: x => x.linkid,
                        principalTable: "link",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userLikes_likeid",
                table: "userLikes",
                column: "likeid");

            migrationBuilder.CreateIndex(
                name: "IX_link_userid",
                table: "link",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_likes_linkid",
                table: "likes",
                column: "linkid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_link_users_userid",
                table: "link",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userLikes_likes_likeid",
                table: "userLikes",
                column: "likeid",
                principalTable: "likes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
