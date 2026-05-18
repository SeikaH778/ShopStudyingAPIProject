using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShopPersistance.Migrations
{
    /// <inheritdoc />
    public partial class RefactorCartAndBuyerInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_BuyerInfo_BuyerId",
                table: "CartItems");

            migrationBuilder.DropTable(
                name: "BuyerInfo");

            migrationBuilder.RenameColumn(
                name: "count",
                table: "Products",
                newName: "Count");

            migrationBuilder.RenameColumn(
                name: "BuyerId",
                table: "CartItems",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_BuyerId",
                table: "CartItems",
                newName: "IX_CartItems_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Users_UserId",
                table: "CartItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Users_UserId",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "Products",
                newName: "count");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CartItems",
                newName: "BuyerId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_UserId",
                table: "CartItems",
                newName: "IX_CartItems_BuyerId");

            migrationBuilder.CreateTable(
                name: "BuyerInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyerInfo_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyerInfo_UserId",
                table: "BuyerInfo",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_BuyerInfo_BuyerId",
                table: "CartItems",
                column: "BuyerId",
                principalTable: "BuyerInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
