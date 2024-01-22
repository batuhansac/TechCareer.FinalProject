using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category_db",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_db", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "User_db",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_db", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Post_db",
                columns: table => new
                {
                    post_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    post_title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    post_content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    post_dateposted = table.Column<short>(type: "smallint", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post_db", x => x.post_id);
                    table.ForeignKey(
                        name: "FK_Post_db_Category_db_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category_db",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Post_db_User_db_UserId",
                        column: x => x.UserId,
                        principalTable: "User_db",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment_db",
                columns: table => new
                {
                    comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comment_content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    comment_dateposted = table.Column<short>(type: "smallint", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment_db", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK_Comment_db_Post_db_PostId",
                        column: x => x.PostId,
                        principalTable: "Post_db",
                        principalColumn: "post_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_db_User_db_UserId",
                        column: x => x.UserId,
                        principalTable: "User_db",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_db_PostId",
                table: "Comment_db",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_db_UserId",
                table: "Comment_db",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_db_CategoryId",
                table: "Post_db",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_db_UserId",
                table: "Post_db",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment_db");

            migrationBuilder.DropTable(
                name: "Post_db");

            migrationBuilder.DropTable(
                name: "Category_db");

            migrationBuilder.DropTable(
                name: "User_db");
        }
    }
}
