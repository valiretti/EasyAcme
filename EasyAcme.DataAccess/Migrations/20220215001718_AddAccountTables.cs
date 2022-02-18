using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyAcme.DataAccess.Migrations
{
    public partial class AddAccountTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcmeAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServerIdentifier = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    AgreementConfirmation = table.Column<bool>(type: "INTEGER", nullable: false),
                    EabKeyIdentifier = table.Column<string>(type: "TEXT", nullable: true),
                    EabKey = table.Column<string>(type: "TEXT", nullable: true),
                    EabKeyAlgorithm = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcmeAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AcmeAccountEmails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    AcmeAccountId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcmeAccountEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcmeAccountEmails_AcmeAccounts_AcmeAccountId",
                        column: x => x.AcmeAccountId,
                        principalTable: "AcmeAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcmeAccountEmails_AcmeAccountId",
                table: "AcmeAccountEmails",
                column: "AcmeAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcmeAccountEmails");

            migrationBuilder.DropTable(
                name: "AcmeAccounts");
        }
    }
}
