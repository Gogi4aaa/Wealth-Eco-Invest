using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wealth_Eco_Invest.Data.Migrations
{
    public partial class addPurchasesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("8e973fa0-ab21-4d45-a0d5-582be213ae22"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("9f9d92f4-2d62-4066-aa13-0b7b9d80f2d0"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("bb00948a-c587-47d2-89d9-9741b15dfd49"));

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnnounceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Announces_AnnounceId",
                        column: x => x.AnnounceId,
                        principalTable: "Announces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("3bbdaaf4-7311-480a-b9b1-c5888231bfc6"), 3, "This rubbish pollutes the environment.You need to throw it in the trash bins!", "https://www.nrdc.org/sites/default/files/styles/medium_16x9_100/public/media-uploads/health4_26_airpollguide_istock_2796602_2400.jpg.jpg?h=c3635fa2&itok=bunvf3B8", 50.00m, new DateTime(2023, 12, 1, 12, 25, 0, 0, DateTimeKind.Unspecified), "Air pollution", new Guid("5168c799-18f9-4c11-ac0a-3f0b210e742d") });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("a750d747-0bf9-4294-b4d6-6c783a7cd34c"), 2, "Water pollution destroy our beaches and oceans.We need to stop it fast!", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdMLiNp09BNzW4jIdGFBpJx4yDwVfeXkygeQ&usqp=CAU", 100000m, new DateTime(2023, 12, 1, 15, 5, 5, 0, DateTimeKind.Unspecified), "Water pollution", new Guid("3a9c9bea-9f47-4924-af37-5079aad72902") });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("fcd90adb-2845-4c2f-9205-e74933cb898f"), 1, "The pollution is the worst thing ever.We need to stop it!", "https://www.interplas.com/product_images/trash-bags/sku/8-10-Gallon-Black-24-x-30-Drawstring-Trash-Bags-1000px.jpg?v=1346367336", 200.00m, new DateTime(2023, 12, 2, 10, 30, 0, 0, DateTimeKind.Unspecified), "Clean the rubbish", new Guid("4f2762fc-4689-4fbd-8e55-8e84eac060cd") });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_AnnounceId",
                table: "Purchases",
                column: "AnnounceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("3bbdaaf4-7311-480a-b9b1-c5888231bfc6"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("a750d747-0bf9-4294-b4d6-6c783a7cd34c"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("fcd90adb-2845-4c2f-9205-e74933cb898f"));

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("8e973fa0-ab21-4d45-a0d5-582be213ae22"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water pollution destroy our beaches and oceans.We need to stop it fast!", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdMLiNp09BNzW4jIdGFBpJx4yDwVfeXkygeQ&usqp=CAU", false, 100000m, new DateTime(2023, 12, 1, 15, 5, 5, 0, DateTimeKind.Unspecified), "Water pollution", new Guid("3a9c9bea-9f47-4924-af37-5079aad72902") });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("9f9d92f4-2d62-4066-aa13-0b7b9d80f2d0"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The pollution is the worst thing ever.We need to stop it!", "https://www.interplas.com/product_images/trash-bags/sku/8-10-Gallon-Black-24-x-30-Drawstring-Trash-Bags-1000px.jpg?v=1346367336", false, 200.00m, new DateTime(2023, 12, 2, 10, 30, 0, 0, DateTimeKind.Unspecified), "Clean the rubbish", new Guid("4f2762fc-4689-4fbd-8e55-8e84eac060cd") });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("bb00948a-c587-47d2-89d9-9741b15dfd49"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This rubbish pollutes the environment.You need to throw it in the trash bins!", "https://www.nrdc.org/sites/default/files/styles/medium_16x9_100/public/media-uploads/health4_26_airpollguide_istock_2796602_2400.jpg.jpg?h=c3635fa2&itok=bunvf3B8", false, 50.00m, new DateTime(2023, 12, 1, 12, 25, 0, 0, DateTimeKind.Unspecified), "Air pollution", new Guid("5168c799-18f9-4c11-ac0a-3f0b210e742d") });
        }
    }
}
