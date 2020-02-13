using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace agregator_linków.Migrations
{
    public partial class Reset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eamil = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    salt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "link",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    url = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    time = table.Column<DateTime>(nullable: false),
                    likeid = table.Column<int>(nullable: false),
                    userid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_link", x => x.id);
                    table.ForeignKey(
                        name: "FK_link_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "likes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    like = table.Column<int>(nullable: false),
                    linkid = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "userLikes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<int>(nullable: false),
                    likeid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userLikes", x => x.id);
                    table.ForeignKey(
                        name: "FK_userLikes_likes_likeid",
                        column: x => x.likeid,
                        principalTable: "likes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_userLikes_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_likes_linkid",
                table: "likes",
                column: "linkid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_link_userid",
                table: "link",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_userLikes_likeid",
                table: "userLikes",
                column: "likeid");

            migrationBuilder.CreateIndex(
                name: "IX_userLikes_userid",
                table: "userLikes",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userLikes");

            migrationBuilder.DropTable(
                name: "likes");

            migrationBuilder.DropTable(
                name: "link");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
