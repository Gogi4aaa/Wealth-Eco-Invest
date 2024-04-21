using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wealth_Eco_Invest.Data.Migrations
{
    public partial class changeUserToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("a58479a1-e0f5-4f4f-9a55-da3ffd62bc72"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("a7ca0d54-62df-447f-b886-2847298abcde"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("bdcb571c-b477-4a6c-9709-b5ca3d27c6bc"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserTo",
                table: "Chats",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("2d55face-ca9a-4015-b3f3-9ba81f0af43d"), 3, "This rubbish pollutes the environment.You need to throw it in the trash bins!", "https://www.nrdc.org/sites/default/files/styles/medium_16x9_100/public/media-uploads/health4_26_airpollguide_istock_2796602_2400.jpg.jpg?h=c3635fa2&itok=bunvf3B8", 50.00m, new DateTime(2023, 12, 1, 12, 25, 0, 0, DateTimeKind.Unspecified), "Air pollution", new Guid("5168c799-18f9-4c11-ac0a-3f0b210e742d") });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("c4c8b110-4a84-4922-9626-c507e29d19cb"), 1, "The pollution is the worst thing ever.We need to stop it!", "https://www.interplas.com/product_images/trash-bags/sku/8-10-Gallon-Black-24-x-30-Drawstring-Trash-Bags-1000px.jpg?v=1346367336", 200.00m, new DateTime(2023, 12, 2, 10, 30, 0, 0, DateTimeKind.Unspecified), "Clean the rubbish", new Guid("4f2762fc-4689-4fbd-8e55-8e84eac060cd") });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("ea244eaf-2677-4e62-8eed-9078056c8df6"), 2, "Water pollution destroy our beaches and oceans.We need to stop it fast!", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdMLiNp09BNzW4jIdGFBpJx4yDwVfeXkygeQ&usqp=CAU", 100000m, new DateTime(2023, 12, 1, 15, 5, 5, 0, DateTimeKind.Unspecified), "Water pollution", new Guid("3a9c9bea-9f47-4924-af37-5079aad72902") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("2d55face-ca9a-4015-b3f3-9ba81f0af43d"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("c4c8b110-4a84-4922-9626-c507e29d19cb"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("ea244eaf-2677-4e62-8eed-9078056c8df6"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserTo",
                table: "Chats",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("a58479a1-e0f5-4f4f-9a55-da3ffd62bc72"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This rubbish pollutes the environment.You need to throw it in the trash bins!", "https://www.nrdc.org/sites/default/files/styles/medium_16x9_100/public/media-uploads/health4_26_airpollguide_istock_2796602_2400.jpg.jpg?h=c3635fa2&itok=bunvf3B8", false, 50.00m, new DateTime(2023, 12, 1, 12, 25, 0, 0, DateTimeKind.Unspecified), "Air pollution", new Guid("5168c799-18f9-4c11-ac0a-3f0b210e742d") });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("a7ca0d54-62df-447f-b886-2847298abcde"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The pollution is the worst thing ever.We need to stop it!", "https://www.interplas.com/product_images/trash-bags/sku/8-10-Gallon-Black-24-x-30-Drawstring-Trash-Bags-1000px.jpg?v=1346367336", false, 200.00m, new DateTime(2023, 12, 2, 10, 30, 0, 0, DateTimeKind.Unspecified), "Clean the rubbish", new Guid("4f2762fc-4689-4fbd-8e55-8e84eac060cd") });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("bdcb571c-b477-4a6c-9709-b5ca3d27c6bc"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water pollution destroy our beaches and oceans.We need to stop it fast!", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdMLiNp09BNzW4jIdGFBpJx4yDwVfeXkygeQ&usqp=CAU", false, 100000m, new DateTime(2023, 12, 1, 15, 5, 5, 0, DateTimeKind.Unspecified), "Water pollution", new Guid("3a9c9bea-9f47-4924-af37-5079aad72902") });
        }
    }
}
