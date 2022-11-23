using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoMute.Web.Migrations
{
    public partial class LinkedCarpoolregistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LinkedCarPoolsId",
                table: "CarPools",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LinkedCarPoolsId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LinkedCarPools",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CarPoolId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedCarPools", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarPools_LinkedCarPoolsId",
                table: "CarPools",
                column: "LinkedCarPoolsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LinkedCarPoolsId",
                table: "AspNetUsers",
                column: "LinkedCarPoolsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_LinkedCarPools_LinkedCarPoolsId",
                table: "AspNetUsers",
                column: "LinkedCarPoolsId",
                principalTable: "LinkedCarPools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarPools_LinkedCarPools_LinkedCarPoolsId",
                table: "CarPools",
                column: "LinkedCarPoolsId",
                principalTable: "LinkedCarPools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_LinkedCarPools_LinkedCarPoolsId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CarPools_LinkedCarPools_LinkedCarPoolsId",
                table: "CarPools");

            migrationBuilder.DropTable(
                name: "LinkedCarPools");

            migrationBuilder.DropIndex(
                name: "IX_CarPools_LinkedCarPoolsId",
                table: "CarPools");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LinkedCarPoolsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LinkedCarPoolsId",
                table: "CarPools");

            migrationBuilder.DropColumn(
                name: "LinkedCarPoolsId",
                table: "AspNetUsers");
        }
    }
}
