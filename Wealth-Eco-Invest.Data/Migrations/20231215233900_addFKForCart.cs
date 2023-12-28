using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wealth_Eco_Invest.Data.Migrations
{
    public partial class addFKForCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("76725405-8ca0-4fdb-8fd2-c2928c9ff4c9"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("b82017e1-edd9-47bc-8436-39039196d198"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("cea0318f-c215-470d-9f39-8898ddd8d8f8"));

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "Title", "UserId" },
                values: new object[] { new Guid("549c0ea1-f577-43df-80a3-8f1292fc7d91"), 1, "The pollution is the worst thing ever.We need to stop it!", "https://www.interplas.com/product_images/trash-bags/sku/8-10-Gallon-Black-24-x-30-Drawstring-Trash-Bags-1000px.jpg?v=1346367336", 200.00m, "Clean the rubbish", new Guid("4f2762fc-4689-4fbd-8e55-8e84eac060cd") });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "Title", "UserId" },
                values: new object[] { new Guid("66f9e150-6505-4512-8b26-49e10dc3cb38"), 3, "This rubbish pollutes the environment.You need to throw it in the trash bins!", "https://www.nrdc.org/sites/default/files/styles/medium_16x9_100/public/media-uploads/health4_26_airpollguide_istock_2796602_2400.jpg.jpg?h=c3635fa2&itok=bunvf3B8", 50.00m, "Air pollution", new Guid("5168c799-18f9-4c11-ac0a-3f0b210e742d") });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "Title", "UserId" },
                values: new object[] { new Guid("a0afae63-429b-414d-a722-beae31fe3ea5"), 2, "Water pollution destroy our beaches and oceans.We need to stop it fast!", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdMLiNp09BNzW4jIdGFBpJx4yDwVfeXkygeQ&usqp=CAU", 100000m, "Water pollution", new Guid("3a9c9bea-9f47-4924-af37-5079aad72902") });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_AnnounceId",
                table: "Carts",
                column: "AnnounceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Announces_AnnounceId",
                table: "Carts",
                column: "AnnounceId",
                principalTable: "Announces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Announces_AnnounceId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_AnnounceId",
                table: "Carts");

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("549c0ea1-f577-43df-80a3-8f1292fc7d91"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("66f9e150-6505-4512-8b26-49e10dc3cb38"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("a0afae63-429b-414d-a722-beae31fe3ea5"));

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "Title", "UserId" },
                values: new object[] { new Guid("76725405-8ca0-4fdb-8fd2-c2928c9ff4c9"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water pollution destroy our beaches and oceans.We need to stop it fast!", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdMLiNp09BNzW4jIdGFBpJx4yDwVfeXkygeQ&usqp=CAU", false, 100000m, "Water pollution", new Guid("3a9c9bea-9f47-4924-af37-5079aad72902") });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "Title", "UserId" },
                values: new object[] { new Guid("b82017e1-edd9-47bc-8436-39039196d198"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This rubbish pollutes the environment.You need to throw it in the trash bins!", "https://www.nrdc.org/sites/default/files/styles/medium_16x9_100/public/media-uploads/health4_26_airpollguide_istock_2796602_2400.jpg.jpg?h=c3635fa2&itok=bunvf3B8", false, 50.00m, "Air pollution", new Guid("5168c799-18f9-4c11-ac0a-3f0b210e742d") });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "Title", "UserId" },
                values: new object[] { new Guid("cea0318f-c215-470d-9f39-8898ddd8d8f8"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The pollution is the worst thing ever.We need to stop it!", "https://www.interplas.com/product_images/trash-bags/sku/8-10-Gallon-Black-24-x-30-Drawstring-Trash-Bags-1000px.jpg?v=1346367336", false, 200.00m, "Clean the rubbish", new Guid("4f2762fc-4689-4fbd-8e55-8e84eac060cd") });
        }
    }
}
