﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ShirazTronic.Data.Migrations
{
    public partial class addProductSpecificationMappingSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductSpecificationMapping",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    SpecificationValueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecificationMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSpecificationMapping_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductSpecificationMapping_SpecificationValue_SpecificationValueId",
                        column: x => x.SpecificationValueId,
                        principalTable: "SpecificationValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecificationMapping_ProductId",
                table: "ProductSpecificationMapping",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecificationMapping_SpecificationValueId",
                table: "ProductSpecificationMapping",
                column: "SpecificationValueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSpecificationMapping");
        }
    }
}
