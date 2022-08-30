using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostgresEF.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WowAuctions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PartitionKey = table.Column<string>(type: "text", nullable: true),
                    AuctionId = table.Column<int>(type: "integer", nullable: false),
                    FirstSeenTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastSeenTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ShortTimeLeftSeen = table.Column<bool>(type: "boolean", nullable: false),
                    Sold = table.Column<bool>(type: "boolean", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<long>(type: "bigint", nullable: false),
                    Buyout = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WowAuctions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WowItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    BonusList = table.Column<string>(type: "text", nullable: true),
                    PetBreedId = table.Column<int>(type: "integer", nullable: true),
                    PetLevel = table.Column<int>(type: "integer", nullable: true),
                    PetQualityId = table.Column<int>(type: "integer", nullable: true),
                    PetSpeciesId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WowItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WowAuctions");

            migrationBuilder.DropTable(
                name: "WowItems");
        }
    }
}
