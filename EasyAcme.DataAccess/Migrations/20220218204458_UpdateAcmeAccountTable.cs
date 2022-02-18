using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyAcme.DataAccess.Migrations
{
    public partial class UpdateAcmeAccountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PemAccountKey",
                table: "AcmeAccounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PemAccountKey",
                table: "AcmeAccounts");
        }
    }
}
