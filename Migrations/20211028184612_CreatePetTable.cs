using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace dotnet_bakery.Migrations
{
    public partial class CreatePetTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PetTable",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    breed = table.Column<int>(type: "integer", nullable: false),
                    color = table.Column<int>(type: "integer", nullable: false),
                    petOwnerId = table.Column<int>(type: "integer", nullable: false),
                    checkedInAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetTable", x => x.id);
                    table.ForeignKey(
                        name: "FK_PetTable_PetOwnerTable_petOwnerId",
                        column: x => x.petOwnerId,
                        principalTable: "PetOwnerTable",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetTable_petOwnerId",
                table: "PetTable",
                column: "petOwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetTable");
        }
    }
}
