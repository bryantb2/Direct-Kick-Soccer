using Microsoft.EntityFrameworkCore.Migrations;

namespace dropShippingApp.Migrations
{
    public partial class teams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Countries_CountryID",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Provinces_ProvidenceProvinceID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamDescription",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamName",
                table: "Teams");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "StreetAddress",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProvidenceProvinceID",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CountryID",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CorporatePageURL",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BusinessEmail",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Teams",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Teams",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Countries_CountryID",
                table: "Teams",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Provinces_ProvidenceProvinceID",
                table: "Teams",
                column: "ProvidenceProvinceID",
                principalTable: "Provinces",
                principalColumn: "ProvinceID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Countries_CountryID",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Provinces_ProvidenceProvinceID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Teams");

            migrationBuilder.AlterColumn<int>(
                name: "ZipCode",
                table: "Teams",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "StreetAddress",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "ProvidenceProvinceID",
                table: "Teams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "Teams",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "CountryID",
                table: "Teams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "CorporatePageURL",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "BusinessEmail",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "TeamDescription",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamName",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Countries_CountryID",
                table: "Teams",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Provinces_ProvidenceProvinceID",
                table: "Teams",
                column: "ProvidenceProvinceID",
                principalTable: "Provinces",
                principalColumn: "ProvinceID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
