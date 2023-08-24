using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planner.Data.Migrations
{
    public partial class CreatedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6b290410-d4e8-49cb-9e43-3f7c578eabcf", 0, "d6ea9f3d-3db3-43a4-8fa2-df8eaf1af03c", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAEGj43NWQ/0K/Lu44aRRuUO43Nte+IC5PsMYjEnhY+VRZtqtofXwwiMOb6ZNho65kcg==", null, false, "74c9417d-c0cf-488d-9f9d-83076d022b7e", false, "test@softuni.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 8, 24, 23, 2, 49, 47, DateTimeKind.Local).AddTicks(1357), "Solve problems, go over past solutions, see most popular interview questions.", "6b290410-d4e8-49cb-9e43-3f7c578eabcf", "Prepare for the technical interview", null },
                    { 2, 1, new DateTime(2023, 2, 5, 23, 2, 49, 47, DateTimeKind.Local).AddTicks(1413), "My dad wants a painting A3 format of their dog or something else.", "6b290410-d4e8-49cb-9e43-3f7c578eabcf", "Draw a painting for uncle's birthday", null },
                    { 3, 2, new DateTime(2023, 2, 5, 23, 2, 49, 47, DateTimeKind.Local).AddTicks(1418), "The last book I bought sits on my sheelf for too long.", "6b290410-d4e8-49cb-9e43-3f7c578eabcf", "Begin reading new book", null },
                    { 4, 3, new DateTime(2023, 2, 5, 23, 2, 49, 47, DateTimeKind.Local).AddTicks(1422), "I filled all of my notebooks and need to buy a new one.", "6b290410-d4e8-49cb-9e43-3f7c578eabcf", "Buy I new notebook for programming", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6b290410-d4e8-49cb-9e43-3f7c578eabcf");
        }
    }
}
