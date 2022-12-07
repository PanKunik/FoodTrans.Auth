using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddingBlockadesIdforusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blockades_Users_UserId",
                table: "Blockades");

            migrationBuilder.DropIndex(
                name: "IX_Blockades_UserId",
                table: "Blockades");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Blockades");

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentBlockadeId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CurrentBlockadeId",
                table: "Users",
                column: "CurrentBlockadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Blockades_CurrentBlockadeId",
                table: "Users",
                column: "CurrentBlockadeId",
                principalTable: "Blockades",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Blockades_CurrentBlockadeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CurrentBlockadeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CurrentBlockadeId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Blockades",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Blockades_UserId",
                table: "Blockades",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blockades_Users_UserId",
                table: "Blockades",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
