using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlinxHub.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderNumber = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CompanyName = table.Column<string>(maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    Surname = table.Column<string>(maxLength: 50, nullable: true),
                    AddresssLine1 = table.Column<string>(maxLength: 50, nullable: true),
                    AddresssLine2 = table.Column<string>(maxLength: 50, nullable: true),
                    Town = table.Column<string>(maxLength: 50, nullable: true),
                    County = table.Column<string>(maxLength: 50, nullable: true),
                    Postcode = table.Column<string>(maxLength: 15, nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    TemplateNumber = table.Column<int>(maxLength: 50, nullable: false),
                    PrimaryBrandColour = table.Column<string>(maxLength: 50, nullable: true),
                    SecondaryBrandColour = table.Column<string>(maxLength: 50, nullable: true),
                    MediumBlogUserName = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderNumber);
                });

            migrationBuilder.CreateTable(
                name: "ApiKey",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    OrderNumber = table.Column<Guid>(nullable: false),
                    OrderNumber1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKey", x => x.Key);
                    table.ForeignKey(
                        name: "FK_ApiKey_Order_OrderNumber1",
                        column: x => x.OrderNumber1,
                        principalTable: "Order",
                        principalColumn: "OrderNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiKey_OrderNumber1",
                table: "ApiKey",
                column: "OrderNumber1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiKey");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
