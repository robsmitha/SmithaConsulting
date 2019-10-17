using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataModeling.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AuthorizationTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizationTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategoryTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategoryTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BlogCommentStatusTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCommentStatusTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BlogStatusTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogStatusTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CardTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CashEventTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashEventTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Percentage = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategoryTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategoryTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ItemTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MerchantTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatusTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentStatusTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatusTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Controller = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PhoneTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PriceTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Abbreviation = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TaskPriorityTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskPriorityTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TaskStatusType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatusType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TaskTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TaxTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TeamTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    StyleSheetCDN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UnitTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VoidReasonTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoidReasonTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Authorizations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Last4 = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    CardType = table.Column<string>(nullable: true),
                    AuthorizationCode = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    AuthorizationTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorizations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Authorizations_AuthorizationTypes_AuthorizationTypeID",
                        column: x => x.AuthorizationTypeID,
                        principalTable: "AuthorizationTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCards",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Last4 = table.Column<string>(nullable: false),
                    First6 = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CardTypeID = table.Column<int>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCards", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerCards_CardTypes_CardTypeID",
                        column: x => x.CardTypeID,
                        principalTable: "CardTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    MerchantName = table.Column<string>(nullable: false),
                    WebsiteUrl = table.Column<string>(nullable: true),
                    MerchantTypeID = table.Column<int>(nullable: false),
                    SelfBoardingApplication = table.Column<bool>(nullable: false),
                    IsBillable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Merchants_MerchantTypes_MerchantTypeID",
                        column: x => x.MerchantTypeID,
                        principalTable: "MerchantTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPhones",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: false),
                    PhoneTypeID = table.Column<int>(nullable: true),
                    Default = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPhones", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerPhones_PhoneTypes_PhoneTypeID",
                        column: x => x.PhoneTypeID,
                        principalTable: "PhoneTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    RoleID = table.Column<int>(nullable: false),
                    PermissionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionID",
                        column: x => x.PermissionID,
                        principalTable: "Permissions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddresses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Street = table.Column<string>(nullable: false),
                    Street2 = table.Column<string>(nullable: true),
                    Secondary = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: false),
                    StateID = table.Column<int>(nullable: false),
                    CountyID = table.Column<int>(nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    Primary = table.Column<bool>(nullable: false),
                    International = table.Column<bool>(nullable: false),
                    AddressTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddresses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_AddressTypes_AddressTypeID",
                        column: x => x.AddressTypeID,
                        principalTable: "AddressTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_Counties_CountyID",
                        column: x => x.CountyID,
                        principalTable: "Counties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_States_StateID",
                        column: x => x.StateID,
                        principalTable: "States",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaxRates",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    TaxAmount = table.Column<decimal>(nullable: true),
                    TaxTypeID = table.Column<int>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaxRates_TaxTypes_TaxTypeID",
                        column: x => x.TaxTypeID,
                        principalTable: "TaxTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ThemeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Applications_Themes_ThemeID",
                        column: x => x.ThemeID,
                        principalTable: "Themes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Subheading = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    BlogStatusTypeID = table.Column<int>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: true),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Blogs_BlogStatusTypes_BlogStatusTypeID",
                        column: x => x.BlogStatusTypeID,
                        principalTable: "BlogStatusTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blogs_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ItemName = table.Column<string>(nullable: false),
                    ItemDescription = table.Column<string>(nullable: false),
                    ItemTypeID = table.Column<int>(nullable: false),
                    MerchantID = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: true),
                    Price = table.Column<decimal>(nullable: true),
                    PriceTypeID = table.Column<int>(nullable: false),
                    UnitTypeID = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Sku = table.Column<string>(nullable: true),
                    DefaultTaxRates = table.Column<bool>(nullable: false),
                    IsRevenue = table.Column<bool>(nullable: false),
                    LookupCode = table.Column<string>(nullable: true),
                    Percentage = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Items_ItemTypes_ItemTypeID",
                        column: x => x.ItemTypeID,
                        principalTable: "ItemTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Merchants_MerchantID",
                        column: x => x.MerchantID,
                        principalTable: "Merchants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_PriceTypes_PriceTypeID",
                        column: x => x.PriceTypeID,
                        principalTable: "PriceTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_UnitTypes_UnitTypeID",
                        column: x => x.UnitTypeID,
                        principalTable: "UnitTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MerchantUsers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    MerchantID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MerchantUsers_Merchants_MerchantID",
                        column: x => x.MerchantID,
                        principalTable: "Merchants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MerchantUsers_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MerchantUsers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Total = table.Column<decimal>(nullable: false),
                    OrderStatusTypeID = table.Column<int>(nullable: false),
                    MerchantID = table.Column<int>(nullable: false),
                    CustomerID = table.Column<int>(nullable: true),
                    UserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Merchants_MerchantID",
                        column: x => x.MerchantID,
                        principalTable: "Merchants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatusTypes_OrderStatusTypeID",
                        column: x => x.OrderStatusTypeID,
                        principalTable: "OrderStatusTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageURL = table.Column<string>(nullable: true),
                    ProjectTypeID = table.Column<int>(nullable: false),
                    MerchantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Projects_Merchants_MerchantID",
                        column: x => x.MerchantID,
                        principalTable: "Merchants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectTypes_ProjectTypeID",
                        column: x => x.ProjectTypeID,
                        principalTable: "ProjectTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageURL = table.Column<string>(nullable: true),
                    TeamTypeID = table.Column<int>(nullable: false),
                    MerchantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Teams_Merchants_MerchantID",
                        column: x => x.MerchantID,
                        principalTable: "Merchants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_TeamTypes_TeamTypeID",
                        column: x => x.TeamTypeID,
                        principalTable: "TeamTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    BlogCategoryTypeID = table.Column<int>(nullable: false),
                    BlogID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BlogCategories_BlogCategoryTypes_BlogCategoryTypeID",
                        column: x => x.BlogCategoryTypeID,
                        principalTable: "BlogCategoryTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogCategories_Blogs_BlogID",
                        column: x => x.BlogID,
                        principalTable: "Blogs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogComments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    BlogID = table.Column<int>(nullable: false),
                    BlogCommentStatusTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BlogComments_BlogCommentStatusTypes_BlogCommentStatusTypeID",
                        column: x => x.BlogCommentStatusTypeID,
                        principalTable: "BlogCommentStatusTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogComments_Blogs_BlogID",
                        column: x => x.BlogID,
                        principalTable: "Blogs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogComments_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ItemCategoryTypeID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItemCategories_ItemCategoryTypes_ItemCategoryTypeID",
                        column: x => x.ItemCategoryTypeID,
                        principalTable: "ItemCategoryTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemCategories_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemImages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    ItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItemImages_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemModifierTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    MaxAllowed = table.Column<int>(nullable: false),
                    MinRequired = table.Column<int>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DefaultModifier = table.Column<bool>(nullable: false),
                    ItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemModifierTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItemModifierTypes_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemStock",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ItemID = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemStock", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItemStock_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemTags",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    TagID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTags", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItemTags_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemTags_Tags_TagID",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    ItemAmount = table.Column<decimal>(nullable: false),
                    ItemID = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LineItems_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LineItems_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    CashTendered = table.Column<decimal>(nullable: true),
                    PaymentTypeID = table.Column<int>(nullable: false),
                    PaymentStatusTypeID = table.Column<int>(nullable: false),
                    AuthorizationID = table.Column<int>(nullable: true),
                    OrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Payments_Authorizations_AuthorizationID",
                        column: x => x.AuthorizationID,
                        principalTable: "Authorizations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentStatusTypes_PaymentStatusTypeID",
                        column: x => x.PaymentStatusTypeID,
                        principalTable: "PaymentStatusTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentTypes_PaymentTypeID",
                        column: x => x.PaymentTypeID,
                        principalTable: "PaymentTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogCommentReplies",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Reply = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    BlogCommentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCommentReplies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BlogCommentReplies_BlogComments_BlogCommentID",
                        column: x => x.BlogCommentID,
                        principalTable: "BlogComments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogCommentReplies_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemModifiers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ItemModifierTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemModifiers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItemModifiers_ItemModifierTypes_ItemModifierTypeID",
                        column: x => x.ItemModifierTypeID,
                        principalTable: "ItemModifierTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CashEvents",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    AmountChange = table.Column<decimal>(nullable: false),
                    CashEventTypeID = table.Column<int>(nullable: false),
                    PaymentID = table.Column<int>(nullable: true),
                    RefundID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashEvents", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CashEvents_CashEventTypes_CashEventTypeID",
                        column: x => x.CashEventTypeID,
                        principalTable: "CashEventTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashEvents_Payments_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "Payments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashEvents_Merchants_RefundID",
                        column: x => x.RefundID,
                        principalTable: "Merchants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Credits",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    VoidReasonTypeID = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: false),
                    PaymentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credits", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Credits_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Credits_Payments_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "Payments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Credits_VoidReasonTypes_VoidReasonTypeID",
                        column: x => x.VoidReasonTypeID,
                        principalTable: "VoidReasonTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Refunds",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    VoidReasonTypeID = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: false),
                    PaymentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refunds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Refunds_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Refunds_Payments_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "Payments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Refunds_VoidReasonTypes_VoidReasonTypeID",
                        column: x => x.VoidReasonTypeID,
                        principalTable: "VoidReasonTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ThemeID",
                table: "Applications",
                column: "ThemeID");

            migrationBuilder.CreateIndex(
                name: "IX_Authorizations_AuthorizationTypeID",
                table: "Authorizations",
                column: "AuthorizationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_BlogCategoryTypeID",
                table: "BlogCategories",
                column: "BlogCategoryTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_BlogID",
                table: "BlogCategories",
                column: "BlogID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCommentReplies_BlogCommentID",
                table: "BlogCommentReplies",
                column: "BlogCommentID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCommentReplies_UserID",
                table: "BlogCommentReplies",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_BlogCommentStatusTypeID",
                table: "BlogComments",
                column: "BlogCommentStatusTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_BlogID",
                table: "BlogComments",
                column: "BlogID");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_UserID",
                table: "BlogComments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogStatusTypeID",
                table: "Blogs",
                column: "BlogStatusTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserID",
                table: "Blogs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CashEvents_CashEventTypeID",
                table: "CashEvents",
                column: "CashEventTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CashEvents_PaymentID",
                table: "CashEvents",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_CashEvents_RefundID",
                table: "CashEvents",
                column: "RefundID");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_OrderID",
                table: "Credits",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_PaymentID",
                table: "Credits",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_VoidReasonTypeID",
                table: "Credits",
                column: "VoidReasonTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_AddressTypeID",
                table: "CustomerAddresses",
                column: "AddressTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_CountyID",
                table: "CustomerAddresses",
                column: "CountyID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_StateID",
                table: "CustomerAddresses",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCards_CardTypeID",
                table: "CustomerCards",
                column: "CardTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPhones_PhoneTypeID",
                table: "CustomerPhones",
                column: "PhoneTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategories_ItemCategoryTypeID",
                table: "ItemCategories",
                column: "ItemCategoryTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategories_ItemID",
                table: "ItemCategories",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemImages_ItemID",
                table: "ItemImages",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemModifiers_ItemModifierTypeID",
                table: "ItemModifiers",
                column: "ItemModifierTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemModifierTypes_ItemID",
                table: "ItemModifierTypes",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTypeID",
                table: "Items",
                column: "ItemTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_MerchantID",
                table: "Items",
                column: "MerchantID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_PriceTypeID",
                table: "Items",
                column: "PriceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UnitTypeID",
                table: "Items",
                column: "UnitTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemStock_ItemID",
                table: "ItemStock",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTags_ItemID",
                table: "ItemTags",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTags_TagID",
                table: "ItemTags",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_ItemID",
                table: "LineItems",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_OrderID",
                table: "LineItems",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_MerchantTypeID",
                table: "Merchants",
                column: "MerchantTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantUsers_MerchantID",
                table: "MerchantUsers",
                column: "MerchantID");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantUsers_RoleID",
                table: "MerchantUsers",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantUsers_UserID",
                table: "MerchantUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MerchantID",
                table: "Orders",
                column: "MerchantID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusTypeID",
                table: "Orders",
                column: "OrderStatusTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AuthorizationID",
                table: "Payments",
                column: "AuthorizationID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderID",
                table: "Payments",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentStatusTypeID",
                table: "Payments",
                column: "PaymentStatusTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentTypeID",
                table: "Payments",
                column: "PaymentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_MerchantID",
                table: "Projects",
                column: "MerchantID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectTypeID",
                table: "Projects",
                column: "ProjectTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_OrderID",
                table: "Refunds",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_PaymentID",
                table: "Refunds",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_VoidReasonTypeID",
                table: "Refunds",
                column: "VoidReasonTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionID",
                table: "RolePermissions",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleID",
                table: "RolePermissions",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_TaxRates_TaxTypeID",
                table: "TaxRates",
                column: "TaxTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_MerchantID",
                table: "Teams",
                column: "MerchantID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamTypeID",
                table: "Teams",
                column: "TeamTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "ApplicationTypes");

            migrationBuilder.DropTable(
                name: "BlogCategories");

            migrationBuilder.DropTable(
                name: "BlogCommentReplies");

            migrationBuilder.DropTable(
                name: "CashEvents");

            migrationBuilder.DropTable(
                name: "Credits");

            migrationBuilder.DropTable(
                name: "CustomerAddresses");

            migrationBuilder.DropTable(
                name: "CustomerCards");

            migrationBuilder.DropTable(
                name: "CustomerPhones");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "ItemCategories");

            migrationBuilder.DropTable(
                name: "ItemImages");

            migrationBuilder.DropTable(
                name: "ItemModifiers");

            migrationBuilder.DropTable(
                name: "ItemStock");

            migrationBuilder.DropTable(
                name: "ItemTags");

            migrationBuilder.DropTable(
                name: "LineItems");

            migrationBuilder.DropTable(
                name: "MerchantUsers");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Refunds");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "TaskPriorityTypes");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskStatusType");

            migrationBuilder.DropTable(
                name: "TaskTypes");

            migrationBuilder.DropTable(
                name: "TaxRates");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropTable(
                name: "BlogCategoryTypes");

            migrationBuilder.DropTable(
                name: "BlogComments");

            migrationBuilder.DropTable(
                name: "CashEventTypes");

            migrationBuilder.DropTable(
                name: "AddressTypes");

            migrationBuilder.DropTable(
                name: "Counties");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "CardTypes");

            migrationBuilder.DropTable(
                name: "PhoneTypes");

            migrationBuilder.DropTable(
                name: "ItemCategoryTypes");

            migrationBuilder.DropTable(
                name: "ItemModifierTypes");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "ProjectTypes");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "VoidReasonTypes");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TaxTypes");

            migrationBuilder.DropTable(
                name: "TeamTypes");

            migrationBuilder.DropTable(
                name: "BlogCommentStatusTypes");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Authorizations");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PaymentStatusTypes");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "BlogStatusTypes");

            migrationBuilder.DropTable(
                name: "ItemTypes");

            migrationBuilder.DropTable(
                name: "PriceTypes");

            migrationBuilder.DropTable(
                name: "UnitTypes");

            migrationBuilder.DropTable(
                name: "AuthorizationTypes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Merchants");

            migrationBuilder.DropTable(
                name: "OrderStatusTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MerchantTypes");
        }
    }
}
