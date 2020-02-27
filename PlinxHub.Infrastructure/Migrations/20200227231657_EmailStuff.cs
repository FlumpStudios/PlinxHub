using Microsoft.EntityFrameworkCore.Migrations;

namespace PlinxHub.Infrastructure.Migrations
{
    public partial class EmailStuff : Migration
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

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "EmailTemplatesId", "Content", "Subject" },
                values: new object[] { 2, @"<p>Thank you very much for ordering your website through Plinx!</p>
                            <p>Your order is being processed and we will be in contact with you shortly</p>
                            <p>Your order reference is {{ORDER}}. If you have any questions, please do not hesitate to contact us at info@plinx-tech.co.uk", "Order confirmation" });

            migrationBuilder.InsertData(
                table: "EmailTemplate",
                columns: new[] { "EmailTemplatesId", "Content", "Subject" },
                values: new object[] { 1, "Someone has placed a new website order", "New order - {{ORDER}}" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplate");
        }
    }
}
