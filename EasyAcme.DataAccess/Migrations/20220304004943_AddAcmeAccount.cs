using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyAcme.DataAccess.Migrations
{
    public partial class AddAcmeAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcmeOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorizationChallengeType = table.Column<string>(type: "TEXT", nullable: false),
                    CommonName = table.Column<string>(type: "TEXT", nullable: false),
                    AcmeAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    AdditionalDomainNames = table.Column<string>(type: "TEXT", nullable: true),
                    CountryName = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<string>(type: "TEXT", nullable: true),
                    Locality = table.Column<string>(type: "TEXT", nullable: true),
                    Organization = table.Column<string>(type: "TEXT", nullable: true),
                    OrganizationUnit = table.Column<string>(type: "TEXT", nullable: true),
                    DnsChallengePlugin = table.Column<string>(type: "TEXT", nullable: true),
                    DnsChallengeSettings = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcmeOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcmeOrders_AcmeAccounts_AcmeAccountId",
                        column: x => x.AcmeAccountId,
                        principalTable: "AcmeAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcmeOrders_AcmeAccountId",
                table: "AcmeOrders",
                column: "AcmeAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcmeOrders");
        }
    }
}
