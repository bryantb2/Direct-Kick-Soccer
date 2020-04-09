using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dropShippingApp.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    ActivityLogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ChangeDescription = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.ActivityLogID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaypalPurchaseId = table.Column<string>(nullable: true),
                    PaypalInvoiceId = table.Column<string>(nullable: true),
                    PaypalOrderId = table.Column<string>(nullable: true),
                    SETrackingId = table.Column<string>(nullable: true),
                    SEReturnTrackingId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "ProductColors",
                columns: table => new
                {
                    ProductColorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsColorActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColors", x => x.ProductColorID);
                });

            migrationBuilder.CreateTable(
                name: "ProductSizes",
                columns: table => new
                {
                    ProductSizeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsSizeActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizes", x => x.ProductSizeID);
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    ProductTagID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagLine = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.ProductTagID);
                });

            migrationBuilder.CreateTable(
                name: "ProprietaryCollections",
                columns: table => new
                {
                    ProprietaryCollectionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true),
                    CategoryDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProprietaryCollections", x => x.ProprietaryCollectionID);
                });

            migrationBuilder.CreateTable(
                name: "QuestionMessages",
                columns: table => new
                {
                    QuestionMessageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionTitle = table.Column<string>(nullable: true),
                    QuestionBody = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    IsResolved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionMessages", x => x.QuestionMessageID);
                });

            migrationBuilder.CreateTable(
                name: "RosterProducts",
                columns: table => new
                {
                    RosterProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelNumber = table.Column<int>(nullable: false),
                    BasePrice = table.Column<decimal>(nullable: false),
                    AddOnPrice = table.Column<decimal>(nullable: false),
                    IsProductActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RosterProducts", x => x.RosterProductID);
                });

            migrationBuilder.CreateTable(
                name: "Providences",
                columns: table => new
                {
                    ProvinceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceName = table.Column<string>(nullable: true),
                    CountryID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providences", x => x.ProvinceID);
                    table.ForeignKey(
                        name: "FK_Providences_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionResponses",
                columns: table => new
                {
                    QuestionResponseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentMessageQuestionMessageID = table.Column<int>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionResponses", x => x.QuestionResponseID);
                    table.ForeignKey(
                        name: "FK_QuestionResponses_QuestionMessages_ParentMessageQuestionMessageID",
                        column: x => x.ParentMessageQuestionMessageID,
                        principalTable: "QuestionMessages",
                        principalColumn: "QuestionMessageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamCreationRequests",
                columns: table => new
                {
                    TeamCreationRequestID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(nullable: true),
                    TeamDescription = table.Column<string>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    CountryID = table.Column<int>(nullable: true),
                    ProvidenceProvinceID = table.Column<int>(nullable: true),
                    CorporatePageURL = table.Column<string>(nullable: true),
                    BusinessEmail = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<int>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamCreationRequests", x => x.TeamCreationRequestID);
                    table.ForeignKey(
                        name: "FK_TeamCreationRequests_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamCreationRequests_Providences_ProvidenceProvinceID",
                        column: x => x.ProvidenceProvinceID,
                        principalTable: "Providences",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(nullable: true),
                    TeamDescription = table.Column<string>(nullable: true),
                    CountryID = table.Column<int>(nullable: true),
                    ProvidenceProvinceID = table.Column<int>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    ZipCode = table.Column<int>(nullable: false),
                    CorporatePageURL = table.Column<string>(nullable: true),
                    BusinessEmail = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<int>(nullable: false),
                    IsTeamInactive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                    table.ForeignKey(
                        name: "FK_Teams_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Providences_ProvidenceProvinceID",
                        column: x => x.ProvidenceProvinceID,
                        principalTable: "Providences",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomProducts",
                columns: table => new
                {
                    CustomProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseProductRosterProductID = table.Column<int>(nullable: true),
                    ProductTitle = table.Column<string>(nullable: true),
                    ProductDescription = table.Column<string>(nullable: true),
                    CustomImagePNG = table.Column<string>(nullable: true),
                    CustomImageSVG = table.Column<string>(nullable: true),
                    IsProductActive = table.Column<bool>(nullable: false),
                    TeamID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomProducts", x => x.CustomProductID);
                    table.ForeignKey(
                        name: "FK_CustomProducts_RosterProducts_BaseProductRosterProductID",
                        column: x => x.BaseProductRosterProductID,
                        principalTable: "RosterProducts",
                        principalColumn: "RosterProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomProducts_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamTags",
                columns: table => new
                {
                    TeamTagID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagLine = table.Column<string>(nullable: true),
                    TeamID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamTags", x => x.TeamTagID);
                    table.ForeignKey(
                        name: "FK_TeamTags_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PricingHistories",
                columns: table => new
                {
                    PricingHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateChanged = table.Column<DateTime>(nullable: false),
                    NewPrice = table.Column<decimal>(nullable: false),
                    CustomProductID = table.Column<int>(nullable: true),
                    RosterProductID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingHistories", x => x.PricingHistoryID);
                    table.ForeignKey(
                        name: "FK_PricingHistories_CustomProducts_CustomProductID",
                        column: x => x.CustomProductID,
                        principalTable: "CustomProducts",
                        principalColumn: "CustomProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PricingHistories_RosterProducts_RosterProductID",
                        column: x => x.RosterProductID,
                        principalTable: "RosterProducts",
                        principalColumn: "RosterProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomProducts_BaseProductRosterProductID",
                table: "CustomProducts",
                column: "BaseProductRosterProductID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomProducts_TeamID",
                table: "CustomProducts",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_PricingHistories_CustomProductID",
                table: "PricingHistories",
                column: "CustomProductID");

            migrationBuilder.CreateIndex(
                name: "IX_PricingHistories_RosterProductID",
                table: "PricingHistories",
                column: "RosterProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Providences_CountryID",
                table: "Providences",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponses_ParentMessageQuestionMessageID",
                table: "QuestionResponses",
                column: "ParentMessageQuestionMessageID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCreationRequests_CountryID",
                table: "TeamCreationRequests",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCreationRequests_ProvidenceProvinceID",
                table: "TeamCreationRequests",
                column: "ProvidenceProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CountryID",
                table: "Teams",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ProvidenceProvinceID",
                table: "Teams",
                column: "ProvidenceProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamTags_TeamID",
                table: "TeamTags",
                column: "TeamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PricingHistories");

            migrationBuilder.DropTable(
                name: "ProductColors");

            migrationBuilder.DropTable(
                name: "ProductSizes");

            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "ProprietaryCollections");

            migrationBuilder.DropTable(
                name: "QuestionResponses");

            migrationBuilder.DropTable(
                name: "TeamCreationRequests");

            migrationBuilder.DropTable(
                name: "TeamTags");

            migrationBuilder.DropTable(
                name: "CustomProducts");

            migrationBuilder.DropTable(
                name: "QuestionMessages");

            migrationBuilder.DropTable(
                name: "RosterProducts");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Providences");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
