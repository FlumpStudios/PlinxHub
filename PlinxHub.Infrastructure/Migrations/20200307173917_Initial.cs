using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlinxHub.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    EmailTemplatesId = table.Column<int>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.EmailTemplatesId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderNumber = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Order_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiKey",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    OrderNumber = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKey", x => x.Key);
                    table.ForeignKey(
                        name: "FK_ApiKey_Order_OrderNumber",
                        column: x => x.OrderNumber,
                        principalTable: "Order",
                        principalColumn: "OrderNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "EmailTemplatesId", "Content", "Subject" },
                values: new object[,]
                {
                    { 2, @"<p>Thank you very much for ordering your website through Plinx!</p>
                                            <p>Your order is being processed and we will be in contact with you shortly</p>
                                            <p>Your order reference is {{ORDER}}. If you have any questions, please do not hesitate to contact us at info@plinx-tech.co.uk", "Order confirmation" },
                    { 1, "Someone has placed a new website order", "New order - {{ORDER}}" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { 1, "Processing Order" },
                    { 2, "In Build" },
                    { 3, "Ready" },
                    { 4, "Live" },
                    { 6, "Cancelled" },
                    { 8, "On Hold" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiKey_OrderNumber",
                table: "ApiKey",
                column: "OrderNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Order_StatusId",
                table: "Order",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiKey");

            migrationBuilder.DropTable(
                name: "EmailTemplate");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
