using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Data.Migrations
{
    public partial class Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "ExistingFilePath",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExistingFilePath_CarId",
                table: "ExistingFilePath",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ExistingFilePath_ProductId",
                table: "ExistingFilePath",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExistingFilePath_Car_CarId",
                table: "ExistingFilePath",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExistingFilePath_Product_ProductId",
                table: "ExistingFilePath",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExistingFilePath_Car_CarId",
                table: "ExistingFilePath");

            migrationBuilder.DropForeignKey(
                name: "FK_ExistingFilePath_Product_ProductId",
                table: "ExistingFilePath");

            migrationBuilder.DropIndex(
                name: "IX_ExistingFilePath_CarId",
                table: "ExistingFilePath");

            migrationBuilder.DropIndex(
                name: "IX_ExistingFilePath_ProductId",
                table: "ExistingFilePath");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "ExistingFilePath");
        }
    }
}
