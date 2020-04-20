using Microsoft.EntityFrameworkCore.Migrations;

namespace dropShippingApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "TeamCreationRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "TeamCreationRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "RosterProducts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "QuestionResponses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "QuestionMessages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "CustomProducts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CartID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DateJoined",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManagedTeamTeamID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "hasApprovedRequest",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ActivityLogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamCreationRequests_AppUserId",
                table: "TeamCreationRequests",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCreationRequests_AppUserId1",
                table: "TeamCreationRequests",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_RosterProducts_AppUserId",
                table: "RosterProducts",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponses_AppUserId",
                table: "QuestionResponses",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionMessages_AppUserId",
                table: "QuestionMessages",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AppUserId",
                table: "Orders",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AppUserId1",
                table: "Orders",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_CustomProducts_AppUserId",
                table: "CustomProducts",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CartID",
                table: "AspNetUsers",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ManagedTeamTeamID",
                table: "AspNetUsers",
                column: "ManagedTeamTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_AppUserId",
                table: "ActivityLogs",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_AppUserId",
                table: "ActivityLogs",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Carts_CartID",
                table: "AspNetUsers",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_ManagedTeamTeamID",
                table: "AspNetUsers",
                column: "ManagedTeamTeamID",
                principalTable: "Teams",
                principalColumn: "TeamID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomProducts_AspNetUsers_AppUserId",
                table: "CustomProducts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_AppUserId",
                table: "Orders",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_AppUserId1",
                table: "Orders",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionMessages_AspNetUsers_AppUserId",
                table: "QuestionMessages",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResponses_AspNetUsers_AppUserId",
                table: "QuestionResponses",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RosterProducts_AspNetUsers_AppUserId",
                table: "RosterProducts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamCreationRequests_AspNetUsers_AppUserId",
                table: "TeamCreationRequests",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamCreationRequests_AspNetUsers_AppUserId1",
                table: "TeamCreationRequests",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_AppUserId",
                table: "ActivityLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Carts_CartID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teams_ManagedTeamTeamID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomProducts_AspNetUsers_AppUserId",
                table: "CustomProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_AppUserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_AppUserId1",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionMessages_AspNetUsers_AppUserId",
                table: "QuestionMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResponses_AspNetUsers_AppUserId",
                table: "QuestionResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_RosterProducts_AspNetUsers_AppUserId",
                table: "RosterProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamCreationRequests_AspNetUsers_AppUserId",
                table: "TeamCreationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamCreationRequests_AspNetUsers_AppUserId1",
                table: "TeamCreationRequests");

            migrationBuilder.DropIndex(
                name: "IX_TeamCreationRequests_AppUserId",
                table: "TeamCreationRequests");

            migrationBuilder.DropIndex(
                name: "IX_TeamCreationRequests_AppUserId1",
                table: "TeamCreationRequests");

            migrationBuilder.DropIndex(
                name: "IX_RosterProducts_AppUserId",
                table: "RosterProducts");

            migrationBuilder.DropIndex(
                name: "IX_QuestionResponses_AppUserId",
                table: "QuestionResponses");

            migrationBuilder.DropIndex(
                name: "IX_QuestionMessages_AppUserId",
                table: "QuestionMessages");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AppUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AppUserId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_CustomProducts_AppUserId",
                table: "CustomProducts");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CartID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ManagedTeamTeamID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_AppUserId",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "TeamCreationRequests");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "TeamCreationRequests");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "RosterProducts");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "QuestionResponses");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "QuestionMessages");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "CustomProducts");

            migrationBuilder.DropColumn(
                name: "CartID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateJoined",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ManagedTeamTeamID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "hasApprovedRequest",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ActivityLogs");
        }
    }
}
