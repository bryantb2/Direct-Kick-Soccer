using Microsoft.EntityFrameworkCore.Migrations;

namespace dropShippingApp.Migrations
{
    public partial class teamMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomProducts_Teams_TeamID",
                table: "CustomProducts");

            migrationBuilder.DropIndex(
                name: "IX_CustomProducts_TeamID",
                table: "CustomProducts");

            migrationBuilder.DropColumn(
                name: "TeamID",
                table: "CustomProducts");

            migrationBuilder.AddColumn<string>(
                name: "TeamBannerPNG",
                table: "Teams",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamBannerPNG",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "TeamID",
                table: "CustomProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomProducts_TeamID",
                table: "CustomProducts",
                column: "TeamID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomProducts_Teams_TeamID",
                table: "CustomProducts",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "TeamID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
