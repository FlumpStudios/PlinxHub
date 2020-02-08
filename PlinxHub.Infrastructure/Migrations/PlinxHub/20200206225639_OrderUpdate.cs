using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlinxHub.Infrastructure.Migrations.PlinxHub
{
    public partial class OrderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
