using Microsoft.EntityFrameworkCore.Migrations;

namespace Web_App_Local.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryDetailsViewId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryDetailsViewId",
                table: "Categories",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryDetailsView",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDetailsView", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryDetailsViewId",
                table: "Products",
                column: "CategoryDetailsViewId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryDetailsViewId",
                table: "Categories",
                column: "CategoryDetailsViewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_CategoryDetailsView_CategoryDetailsViewId",
                table: "Categories",
                column: "CategoryDetailsViewId",
                principalTable: "CategoryDetailsView",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CategoryDetailsView_CategoryDetailsViewId",
                table: "Products",
                column: "CategoryDetailsViewId",
                principalTable: "CategoryDetailsView",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_CategoryDetailsView_CategoryDetailsViewId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoryDetailsView_CategoryDetailsViewId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "CategoryDetailsView");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryDetailsViewId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryDetailsViewId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryDetailsViewId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryDetailsViewId",
                table: "Categories");
        }
    }
}
