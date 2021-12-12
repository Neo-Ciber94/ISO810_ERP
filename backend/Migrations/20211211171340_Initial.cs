using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISO810_ERP.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomePage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Name", "ShortCode", "Symbol" },
                values: new object[,]
                {
                    { 1, "Dominican Peso", "DOP", "RD$" },
                    { 2, "US Dollar", "USD", "$" },
                    { 3, "Euro", "EUR", "€" },
                    { 4, "Pound Sterling", "GBP", "£" }
                });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Customer Support" },
                    { 2, "Development / R&D" },
                    { 3, "Marketing" },
                    { 4, "Ops" },
                    { 5, "Web Hosting" },
                    { 6, "Productivity" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "HomePage", "Name" },
                values: new object[,]
                {
                    { 1, "https://www.mongodb.com/atlas", "MongoDB" },
                    { 2, "https://monday.com/", "Monday" },
                    { 3, "https://circleci.com/", "CircleCI" },
                    { 4, "https://filestage.io/", "Filestage" },
                    { 5, "https://www.hubspot.com/", "Hubspot" },
                    { 6, "https://balsamiq.com/", "Balsamiq" },
                    { 7, "https://www.adobe.com/", "Adobe" },
                    { 8, "https://www.abstract.com/", "Abstract" },
                    { 9, "https://www.algolia.com/", "Algolia" },
                    { 10, "https://aws.amazon.com/", "AWS" },
                    { 11, "https://www.hetzner.com/cloud", "Hetzner" },
                    { 12, "https://cloud.google.com/", "Google Cloud" },
                    { 13, "https://azure.microsoft.com/en-us/", "Azure" },
                    { 14, "https://www.vultr.com/", "Vultr" },
                    { 15, "https://www.paypal.com/", "PayPal" },
                    { 16, "https://www.digitalocean.com/", "Digital Ocean" },
                    { 17, "https://www.netcup.eu/", "Netcup" },
                    { 18, "https://stripe.com/", "Stripe" },
                    { 19, "https://squareup.com/", "Square" },
                    { 20, "https://commerce.coinbase.com/", "Coinbase Commerce" },
                    { 21, "https://www.linode.com/", "Linode" },
                    { 22, "https://mailchimp.com/", "Mailchimp" },
                    { 23, "https://www.azul.com.do/Pages/es/default.aspx", "Azul dominicana" },
                    { 24, "https://www.cardnet.com.do/", "Cardnet dominicana" },
                    { 25, "https://www.godaddy.com/", "Godaddy" },
                    { 26, "https://github.com/", "Github" },
                    { 27, "https://about.gitlab.com/", "Gitlab" },
                    { 28, "https://vercel.com/", "Vercel" },
                    { 29, "https://www.netlify.com/", "Netifly" },
                    { 30, "https://www.cloudflare.com/", "Cloudflare" },
                    { 31, "https://www.figma.com/", "Figma" },
                    { 32, "https://business.facebook.com/", "Facebook Business" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "HomePage", "Name" },
                values: new object[] { 33, "https://ads.google.com/home/", "Google Ads" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "HomePage", "Name" },
                values: new object[] { 34, "https://www.intercom.com/", "Intercom" });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CurrencyId",
                table: "Expenses",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_OrganizationId",
                table: "Expenses",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ServiceId",
                table: "Expenses",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_AccountId",
                table: "Organizations",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseCategories");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
