﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dropShippingApp.Data;

namespace dropShippingApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("dropShippingApp.Models.ActivityLog", b =>
                {
                    b.Property<int>("ActivityLogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChangeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActivityLogID");

                    b.HasIndex("AppUserId");

                    b.ToTable("ActivityLogs");
                });

            modelBuilder.Entity("dropShippingApp.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("CartID")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DateJoined")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("ManagedTeamTeamID")
                        .HasColumnType("int");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("hasApprovedRequest")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CartID");

                    b.HasIndex("ManagedTeamTeamID");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("dropShippingApp.Models.Cart", b =>
                {
                    b.Property<int>("CartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("SessionID")
                        .HasColumnType("int");

                    b.HasKey("CartID");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("dropShippingApp.Models.CartItem", b =>
                {
                    b.Property<int>("CartItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CartID")
                        .HasColumnType("int");

                    b.Property<int?>("ProductSelectionCustomProductID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CartItemID");

                    b.HasIndex("CartID");

                    b.HasIndex("ProductSelectionCustomProductID");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("dropShippingApp.Models.Country", b =>
                {
                    b.Property<int>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("dropShippingApp.Models.CustomProduct", b =>
                {
                    b.Property<int>("CustomProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BaseProductRosterProductID")
                        .HasColumnType("int");

                    b.Property<string>("CustomImagePNG")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomImageSVG")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsProductActive")
                        .HasColumnType("bit");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeamID")
                        .HasColumnType("int");

                    b.HasKey("CustomProductID");

                    b.HasIndex("AppUserId");

                    b.HasIndex("BaseProductRosterProductID");

                    b.HasIndex("TeamID");

                    b.ToTable("CustomProducts");
                });

            modelBuilder.Entity("dropShippingApp.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AppUserId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PaypalOrderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ReturnRequested")
                        .HasColumnType("bit");

                    b.Property<string>("SEReturnTrackingId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SETrackingId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderID");

                    b.HasIndex("AppUserId");

                    b.HasIndex("AppUserId1");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("dropShippingApp.Models.PricingHistory", b =>
                {
                    b.Property<int>("PricingHistoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomProductID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateChanged")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("NewPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("RosterProductID")
                        .HasColumnType("int");

                    b.HasKey("PricingHistoryID");

                    b.HasIndex("CustomProductID");

                    b.HasIndex("RosterProductID");

                    b.ToTable("PricingHistories");
                });

            modelBuilder.Entity("dropShippingApp.Models.ProductCategory", b =>
                {
                    b.Property<int>("ProductCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductCategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("dropShippingApp.Models.ProductColor", b =>
                {
                    b.Property<int>("ProductColorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ColorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsColorActive")
                        .HasColumnType("bit");

                    b.HasKey("ProductColorID");

                    b.ToTable("ProductColors");
                });

            modelBuilder.Entity("dropShippingApp.Models.ProductSize", b =>
                {
                    b.Property<int>("ProductSizeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsSizeActive")
                        .HasColumnType("bit");

                    b.Property<string>("SizeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductSizeID");

                    b.ToTable("ProductSizes");
                });

            modelBuilder.Entity("dropShippingApp.Models.ProductSort", b =>
                {
                    b.Property<int>("ProductSortID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SortName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductSortID");

                    b.ToTable("ProductSorts");
                });

            modelBuilder.Entity("dropShippingApp.Models.Province", b =>
                {
                    b.Property<int>("ProvinceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryID")
                        .HasColumnType("int");

                    b.Property<string>("ProvinceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProvinceID");

                    b.HasIndex("CountryID");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("dropShippingApp.Models.QuestionMessage", b =>
                {
                    b.Property<int>("QuestionMessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsResolved")
                        .HasColumnType("bit");

                    b.Property<string>("QuestionBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("QuestionMessageID");

                    b.HasIndex("AppUserId");

                    b.ToTable("QuestionMessages");
                });

            modelBuilder.Entity("dropShippingApp.Models.QuestionResponse", b =>
                {
                    b.Property<int>("QuestionResponseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ParentMessageQuestionMessageID")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("QuestionResponseID");

                    b.HasIndex("AppUserId");

                    b.HasIndex("ParentMessageQuestionMessageID");

                    b.ToTable("QuestionResponses");
                });

            modelBuilder.Entity("dropShippingApp.Models.RosterProduct", b =>
                {
                    b.Property<int>("RosterProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BaseColorProductColorID")
                        .HasColumnType("int");

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("BaseSizeProductSizeID")
                        .HasColumnType("int");

                    b.Property<int>("CategoryProductCategoryID")
                        .HasColumnType("int");

                    b.Property<bool>("IsProductActive")
                        .HasColumnType("bit");

                    b.Property<int>("ModelNumber")
                        .HasColumnType("int");

                    b.Property<int>("SKU")
                        .HasColumnType("int");

                    b.HasKey("RosterProductID");

                    b.HasIndex("AppUserId");

                    b.HasIndex("BaseColorProductColorID");

                    b.HasIndex("BaseSizeProductSizeID");

                    b.HasIndex("CategoryProductCategoryID");

                    b.ToTable("RosterProducts");
                });

            modelBuilder.Entity("dropShippingApp.Models.Tag", b =>
                {
                    b.Property<int>("TagID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomProductID")
                        .HasColumnType("int");

                    b.Property<int?>("RosterProductID")
                        .HasColumnType("int");

                    b.Property<string>("TagLine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeamID")
                        .HasColumnType("int");

                    b.HasKey("TagID");

                    b.HasIndex("CustomProductID");

                    b.HasIndex("RosterProductID");

                    b.HasIndex("TeamID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("dropShippingApp.Models.Team", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BusinessEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorporatePageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsHostTeam")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTeamInactive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProvidenceProvinceID")
                        .HasColumnType("int");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeamID");

                    b.HasIndex("CountryID");

                    b.HasIndex("ProvidenceProvinceID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("dropShippingApp.Models.TeamCreationRequest", b =>
                {
                    b.Property<int>("TeamCreationRequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AppUserId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BusinessEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorporatePageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CountryID")
                        .HasColumnType("int");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<int?>("ProvidenceProvinceID")
                        .HasColumnType("int");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeamCreationRequestID");

                    b.HasIndex("AppUserId");

                    b.HasIndex("AppUserId1");

                    b.HasIndex("CountryID");

                    b.HasIndex("ProvidenceProvinceID");

                    b.ToTable("TeamCreationRequests");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("dropShippingApp.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("dropShippingApp.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dropShippingApp.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("dropShippingApp.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("dropShippingApp.Models.ActivityLog", b =>
                {
                    b.HasOne("dropShippingApp.Models.AppUser", null)
                        .WithMany("ActivityLog")
                        .HasForeignKey("AppUserId");
                });

            modelBuilder.Entity("dropShippingApp.Models.AppUser", b =>
                {
                    b.HasOne("dropShippingApp.Models.Cart", "Cart")
                        .WithMany()
                        .HasForeignKey("CartID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dropShippingApp.Models.Team", "ManagedTeam")
                        .WithMany()
                        .HasForeignKey("ManagedTeamTeamID");
                });

            modelBuilder.Entity("dropShippingApp.Models.CartItem", b =>
                {
                    b.HasOne("dropShippingApp.Models.Cart", null)
                        .WithMany("CartItems")
                        .HasForeignKey("CartID");

                    b.HasOne("dropShippingApp.Models.CustomProduct", "ProductSelection")
                        .WithMany()
                        .HasForeignKey("ProductSelectionCustomProductID");
                });

            modelBuilder.Entity("dropShippingApp.Models.CustomProduct", b =>
                {
                    b.HasOne("dropShippingApp.Models.AppUser", null)
                        .WithMany("CreatedCustomProducts")
                        .HasForeignKey("AppUserId");

                    b.HasOne("dropShippingApp.Models.RosterProduct", "BaseProduct")
                        .WithMany()
                        .HasForeignKey("BaseProductRosterProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dropShippingApp.Models.Team", null)
                        .WithMany("TeamProducts")
                        .HasForeignKey("TeamID");
                });

            modelBuilder.Entity("dropShippingApp.Models.Order", b =>
                {
                    b.HasOne("dropShippingApp.Models.AppUser", null)
                        .WithMany("ApprovedOrders")
                        .HasForeignKey("AppUserId");

                    b.HasOne("dropShippingApp.Models.AppUser", null)
                        .WithMany("UserOrderHistory")
                        .HasForeignKey("AppUserId1");
                });

            modelBuilder.Entity("dropShippingApp.Models.PricingHistory", b =>
                {
                    b.HasOne("dropShippingApp.Models.CustomProduct", null)
                        .WithMany("PricingHistory")
                        .HasForeignKey("CustomProductID");

                    b.HasOne("dropShippingApp.Models.RosterProduct", null)
                        .WithMany("PricingHistory")
                        .HasForeignKey("RosterProductID");
                });

            modelBuilder.Entity("dropShippingApp.Models.Province", b =>
                {
                    b.HasOne("dropShippingApp.Models.Country", null)
                        .WithMany("Providences")
                        .HasForeignKey("CountryID");
                });

            modelBuilder.Entity("dropShippingApp.Models.QuestionMessage", b =>
                {
                    b.HasOne("dropShippingApp.Models.AppUser", null)
                        .WithMany("AskedQuestions")
                        .HasForeignKey("AppUserId");
                });

            modelBuilder.Entity("dropShippingApp.Models.QuestionResponse", b =>
                {
                    b.HasOne("dropShippingApp.Models.AppUser", null)
                        .WithMany("AnsweredQuestions")
                        .HasForeignKey("AppUserId");

                    b.HasOne("dropShippingApp.Models.QuestionMessage", "ParentMessage")
                        .WithMany()
                        .HasForeignKey("ParentMessageQuestionMessageID");
                });

            modelBuilder.Entity("dropShippingApp.Models.RosterProduct", b =>
                {
                    b.HasOne("dropShippingApp.Models.AppUser", null)
                        .WithMany("RosterProducts")
                        .HasForeignKey("AppUserId");

                    b.HasOne("dropShippingApp.Models.ProductColor", "BaseColor")
                        .WithMany()
                        .HasForeignKey("BaseColorProductColorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dropShippingApp.Models.ProductSize", "BaseSize")
                        .WithMany()
                        .HasForeignKey("BaseSizeProductSizeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dropShippingApp.Models.ProductCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryProductCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("dropShippingApp.Models.Tag", b =>
                {
                    b.HasOne("dropShippingApp.Models.CustomProduct", null)
                        .WithMany("ProductTags")
                        .HasForeignKey("CustomProductID");

                    b.HasOne("dropShippingApp.Models.RosterProduct", null)
                        .WithMany("ProductTags")
                        .HasForeignKey("RosterProductID");

                    b.HasOne("dropShippingApp.Models.Team", null)
                        .WithMany("TeamTags")
                        .HasForeignKey("TeamID");
                });

            modelBuilder.Entity("dropShippingApp.Models.Team", b =>
                {
                    b.HasOne("dropShippingApp.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dropShippingApp.Models.Province", "Providence")
                        .WithMany()
                        .HasForeignKey("ProvidenceProvinceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("dropShippingApp.Models.TeamCreationRequest", b =>
                {
                    b.HasOne("dropShippingApp.Models.AppUser", null)
                        .WithMany("ApprovedTeamRequests")
                        .HasForeignKey("AppUserId");

                    b.HasOne("dropShippingApp.Models.AppUser", null)
                        .WithMany("CreationRequestHistory")
                        .HasForeignKey("AppUserId1");

                    b.HasOne("dropShippingApp.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID");

                    b.HasOne("dropShippingApp.Models.Province", "Providence")
                        .WithMany()
                        .HasForeignKey("ProvidenceProvinceID");
                });
#pragma warning restore 612, 618
        }
    }
}
