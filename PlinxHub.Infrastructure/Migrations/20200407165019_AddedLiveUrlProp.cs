using Microsoft.EntityFrameworkCore.Migrations;

namespace PlinxHub.Infrastructure.Migrations
{
    public partial class AddedLiveUrlProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LiveUrl",
                table: "Order",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LiveUrl",
                table: "Order");
        }
    }
}
