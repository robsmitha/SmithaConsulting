using Microsoft.EntityFrameworkCore.Migrations;

namespace Architecture.Migrations
{
    public partial class ApplicationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationTypeID",
                table: "Applications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicationTypeID",
                table: "Applications",
                column: "ApplicationTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationTypes_ApplicationTypeID",
                table: "Applications",
                column: "ApplicationTypeID",
                principalTable: "ApplicationTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationTypes_ApplicationTypeID",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_ApplicationTypeID",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ApplicationTypeID",
                table: "Applications");
        }
    }
}
