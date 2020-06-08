using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dropShippingApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartID);
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
                name: "ImgurConfiguration",
                columns: table => new
                {
                    ImgurConfigID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessToken = table.Column<string>(nullable: true),
                    AccessLastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImgurConfiguration", x => x.ImgurConfigID);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ProductCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.ProductCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "ProductColors",
                columns: table => new
                {
                    ProductColorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsColorActive = table.Column<bool>(nullable: false),
                    ColorName = table.Column<string>(nullable: false)
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
                    IsSizeActive = table.Column<bool>(nullable: false),
                    SizeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizes", x => x.ProductSizeID);
                });

            migrationBuilder.CreateTable(
                name: "ProductSorts",
                columns: table => new
                {
                    SortID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSorts", x => x.SortID);
                });

            migrationBuilder.CreateTable(
                name: "SavedImgurPhotos",
                columns: table => new
                {
                    ImgurPhotoDataID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoID = table.Column<string>(nullable: true),
                    DeleteHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedImgurPhotos", x => x.ImgurPhotoDataID);
                });

            migrationBuilder.CreateTable(
                name: "TeamCategories",
                columns: table => new
                {
                    TeamCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamCategories", x => x.TeamCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "TeamSorts",
                columns: table => new
                {
                    SortID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamSorts", x => x.SortID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ProvinceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceName = table.Column<string>(nullable: true),
                    ProvienceAbbreviation = table.Column<string>(nullable: true),
                    CountryID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ProvinceID);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RosterGroups",
                columns: table => new
                {
                    RosterGroupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    ModelNumber = table.Column<int>(nullable: false),
                    GeneralThumbnail = table.Column<string>(nullable: false),
                    CategoryProductCategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RosterGroups", x => x.RosterGroupID);
                    table.ForeignKey(
                        name: "FK_RosterGroups_ProductCategories_CategoryProductCategoryID",
                        column: x => x.CategoryProductCategoryID,
                        principalTable: "ProductCategories",
                        principalColumn: "ProductCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    CountryID = table.Column<int>(nullable: false),
                    ProvidenceProvinceID = table.Column<int>(nullable: false),
                    StreetAddress = table.Column<string>(nullable: false),
                    ZipCode = table.Column<string>(nullable: false),
                    CorporatePageURL = table.Column<string>(nullable: false),
                    BusinessEmail = table.Column<string>(nullable: false),
                    BannerImageDataImgurPhotoDataID = table.Column<int>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: false),
                    IsTeamInactive = table.Column<bool>(nullable: false),
                    IsHostTeam = table.Column<bool>(nullable: false),
                    DateJoined = table.Column<DateTime>(nullable: false),
                    CategoryTeamCategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                    table.ForeignKey(
                        name: "FK_Teams_SavedImgurPhotos_BannerImageDataImgurPhotoDataID",
                        column: x => x.BannerImageDataImgurPhotoDataID,
                        principalTable: "SavedImgurPhotos",
                        principalColumn: "ImgurPhotoDataID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_TeamCategories_CategoryTeamCategoryID",
                        column: x => x.CategoryTeamCategoryID,
                        principalTable: "TeamCategories",
                        principalColumn: "TeamCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Provinces_ProvidenceProvinceID",
                        column: x => x.ProvidenceProvinceID,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    DateJoined = table.Column<long>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    ManagedTeamTeamID = table.Column<int>(nullable: true),
                    CartID = table.Column<int>(nullable: false),
                    hasApprovedRequest = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Carts_CartID",
                        column: x => x.CartID,
                        principalTable: "Carts",
                        principalColumn: "CartID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Teams_ManagedTeamTeamID",
                        column: x => x.ManagedTeamTeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroups",
                columns: table => new
                {
                    ProductGroupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneralThumbnail = table.Column<string>(nullable: true),
                    ImgurImageID = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    PrintDesignPNG = table.Column<string>(nullable: false),
                    BaseGroupModelNumber = table.Column<int>(nullable: false),
                    TeamID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroups", x => x.ProductGroupID);
                    table.ForeignKey(
                        name: "FK_ProductGroups_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    ActivityLogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ChangeDescription = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.ActivityLogID);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatePlaced = table.Column<DateTime>(nullable: false),
                    PaypalOrderId = table.Column<string>(nullable: true),
                    SETrackingId = table.Column<string>(nullable: true),
                    SEReturnTrackingId = table.Column<string>(nullable: true),
                    ReturnRequested = table.Column<bool>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    AppUserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsResolved = table.Column<bool>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionMessages", x => x.QuestionMessageID);
                    table.ForeignKey(
                        name: "FK_QuestionMessages_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RosterProducts",
                columns: table => new
                {
                    RosterProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsProductActive = table.Column<bool>(nullable: false),
                    SKU = table.Column<int>(nullable: false),
                    BaseColorProductColorID = table.Column<int>(nullable: false),
                    BaseSizeProductSizeID = table.Column<int>(nullable: false),
                    RosterGroupID = table.Column<int>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RosterProducts", x => x.RosterProductID);
                    table.ForeignKey(
                        name: "FK_RosterProducts_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RosterProducts_ProductColors_BaseColorProductColorID",
                        column: x => x.BaseColorProductColorID,
                        principalTable: "ProductColors",
                        principalColumn: "ProductColorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RosterProducts_ProductSizes_BaseSizeProductSizeID",
                        column: x => x.BaseSizeProductSizeID,
                        principalTable: "ProductSizes",
                        principalColumn: "ProductSizeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RosterProducts_RosterGroups_RosterGroupID",
                        column: x => x.RosterGroupID,
                        principalTable: "RosterGroups",
                        principalColumn: "RosterGroupID",
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
                    PhoneNumber = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    AppUserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamCreationRequests", x => x.TeamCreationRequestID);
                    table.ForeignKey(
                        name: "FK_TeamCreationRequests_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamCreationRequests_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamCreationRequests_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamCreationRequests_Provinces_ProvidenceProvinceID",
                        column: x => x.ProvidenceProvinceID,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagLine = table.Column<string>(nullable: true),
                    ProductGroupID = table.Column<int>(nullable: true),
                    RosterGroupID = table.Column<int>(nullable: true),
                    TeamID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagID);
                    table.ForeignKey(
                        name: "FK_Tags_ProductGroups_ProductGroupID",
                        column: x => x.ProductGroupID,
                        principalTable: "ProductGroups",
                        principalColumn: "ProductGroupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tags_RosterGroups_RosterGroupID",
                        column: x => x.RosterGroupID,
                        principalTable: "RosterGroups",
                        principalColumn: "RosterGroupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tags_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductFamilyID = table.Column<string>(nullable: true),
                    ProductID = table.Column<string>(nullable: true),
                    TeamID = table.Column<string>(nullable: true),
                    OrderID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemID);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionResponses",
                columns: table => new
                {
                    QuestionResponseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentMessageQuestionMessageID = table.Column<int>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionResponses", x => x.QuestionResponseID);
                    table.ForeignKey(
                        name: "FK_QuestionResponses_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionResponses_QuestionMessages_ParentMessageQuestionMessageID",
                        column: x => x.ParentMessageQuestionMessageID,
                        principalTable: "QuestionMessages",
                        principalColumn: "QuestionMessageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomProducts",
                columns: table => new
                {
                    CustomProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductPNG = table.Column<string>(nullable: true),
                    ImgurImageID = table.Column<string>(nullable: true),
                    IsProductActive = table.Column<bool>(nullable: false),
                    BaseProductRosterProductID = table.Column<int>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    ProductGroupID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomProducts", x => x.CustomProductID);
                    table.ForeignKey(
                        name: "FK_CustomProducts_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomProducts_RosterProducts_BaseProductRosterProductID",
                        column: x => x.BaseProductRosterProductID,
                        principalTable: "RosterProducts",
                        principalColumn: "RosterProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomProducts_ProductGroups_ProductGroupID",
                        column: x => x.ProductGroupID,
                        principalTable: "ProductGroups",
                        principalColumn: "ProductGroupID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSelectionCustomProductID = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    CartID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemID);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartID",
                        column: x => x.CartID,
                        principalTable: "Carts",
                        principalColumn: "CartID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartItems_CustomProducts_ProductSelectionCustomProductID",
                        column: x => x.ProductSelectionCustomProductID,
                        principalTable: "CustomProducts",
                        principalColumn: "CustomProductID",
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
                name: "IX_ActivityLogs_AppUserId",
                table: "ActivityLogs",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CartID",
                table: "AspNetUsers",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ManagedTeamTeamID",
                table: "AspNetUsers",
                column: "ManagedTeamTeamID");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartID",
                table: "CartItems",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductSelectionCustomProductID",
                table: "CartItems",
                column: "ProductSelectionCustomProductID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomProducts_AppUserId",
                table: "CustomProducts",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomProducts_BaseProductRosterProductID",
                table: "CustomProducts",
                column: "BaseProductRosterProductID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomProducts_ProductGroupID",
                table: "CustomProducts",
                column: "ProductGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderID",
                table: "OrderItems",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AppUserId",
                table: "Orders",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AppUserId1",
                table: "Orders",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_PricingHistories_CustomProductID",
                table: "PricingHistories",
                column: "CustomProductID");

            migrationBuilder.CreateIndex(
                name: "IX_PricingHistories_RosterProductID",
                table: "PricingHistories",
                column: "RosterProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_TeamID",
                table: "ProductGroups",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryID",
                table: "Provinces",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionMessages_AppUserId",
                table: "QuestionMessages",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponses_AppUserId",
                table: "QuestionResponses",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponses_ParentMessageQuestionMessageID",
                table: "QuestionResponses",
                column: "ParentMessageQuestionMessageID");

            migrationBuilder.CreateIndex(
                name: "IX_RosterGroups_CategoryProductCategoryID",
                table: "RosterGroups",
                column: "CategoryProductCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_RosterProducts_AppUserId",
                table: "RosterProducts",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RosterProducts_BaseColorProductColorID",
                table: "RosterProducts",
                column: "BaseColorProductColorID");

            migrationBuilder.CreateIndex(
                name: "IX_RosterProducts_BaseSizeProductSizeID",
                table: "RosterProducts",
                column: "BaseSizeProductSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_RosterProducts_RosterGroupID",
                table: "RosterProducts",
                column: "RosterGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ProductGroupID",
                table: "Tags",
                column: "ProductGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_RosterGroupID",
                table: "Tags",
                column: "RosterGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TeamID",
                table: "Tags",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCreationRequests_AppUserId",
                table: "TeamCreationRequests",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCreationRequests_AppUserId1",
                table: "TeamCreationRequests",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCreationRequests_CountryID",
                table: "TeamCreationRequests",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCreationRequests_ProvidenceProvinceID",
                table: "TeamCreationRequests",
                column: "ProvidenceProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_BannerImageDataImgurPhotoDataID",
                table: "Teams",
                column: "BannerImageDataImgurPhotoDataID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CategoryTeamCategoryID",
                table: "Teams",
                column: "CategoryTeamCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CountryID",
                table: "Teams",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ProvidenceProvinceID",
                table: "Teams",
                column: "ProvidenceProvinceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "ImgurConfiguration");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "PricingHistories");

            migrationBuilder.DropTable(
                name: "ProductSorts");

            migrationBuilder.DropTable(
                name: "QuestionResponses");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "TeamCreationRequests");

            migrationBuilder.DropTable(
                name: "TeamSorts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "CustomProducts");

            migrationBuilder.DropTable(
                name: "QuestionMessages");

            migrationBuilder.DropTable(
                name: "RosterProducts");

            migrationBuilder.DropTable(
                name: "ProductGroups");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ProductColors");

            migrationBuilder.DropTable(
                name: "ProductSizes");

            migrationBuilder.DropTable(
                name: "RosterGroups");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "SavedImgurPhotos");

            migrationBuilder.DropTable(
                name: "TeamCategories");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
