using Microsoft.EntityFrameworkCore.Migrations;

namespace PlinxHub.Infrastructure.Migrations
{
    public partial class FkeyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ApiKey_OrderNumber",
                table: "ApiKey",
                column: "OrderNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiKey_Order_OrderNumber",
                table: "ApiKey",
                column: "OrderNumber",
                principalTable: "Order",
                principalColumn: "OrderNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiKey_Order_OrderNumber",
                table: "ApiKey");

            migrationBuilder.DropIndex(
                name: "IX_ApiKey_OrderNumber",
                table: "ApiKey");
        }
    }
}
