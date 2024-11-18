using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.src.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationshipsCartDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_Products_ProductId",
                table: "CartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_ShoppingCarts_ShoppingCartCartId",
                table: "CartDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartDetails_ShoppingCartCartId",
                table: "CartDetails");

            migrationBuilder.DropColumn(
                name: "ShoppingCartCartId",
                table: "CartDetails");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_CartId",
                table: "CartDetails",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_Products_ProductId",
                table: "CartDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_ShoppingCarts_CartId",
                table: "CartDetails",
                column: "CartId",
                principalTable: "ShoppingCarts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_Products_ProductId",
                table: "CartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_ShoppingCarts_CartId",
                table: "CartDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartDetails_CartId",
                table: "CartDetails");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartCartId",
                table: "CartDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_ShoppingCartCartId",
                table: "CartDetails",
                column: "ShoppingCartCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_Products_ProductId",
                table: "CartDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_ShoppingCarts_ShoppingCartCartId",
                table: "CartDetails",
                column: "ShoppingCartCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
