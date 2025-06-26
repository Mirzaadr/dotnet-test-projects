using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CustomerApp.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customerid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customercode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    customername = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    customeraddress = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false, defaultValueSql: "''::character varying"),
                    createdby = table.Column<int>(type: "integer", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modifiedby = table.Column<int>(type: "integer", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("customer_pkey", x => x.customerid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer");
        }
    }
}
