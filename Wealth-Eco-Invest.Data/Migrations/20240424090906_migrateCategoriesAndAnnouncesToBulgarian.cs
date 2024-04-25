using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wealth_Eco_Invest.Data.Migrations
{
    public partial class migrateCategoriesAndAnnouncesToBulgarian : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "StartDate", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("25d38bb2-92c0-4c11-8025-e856c96e7652"), 3, "Тези боклуци замърсяват околната среда. Трябва да ги изхвърлите в кофите за боклук!", "https://www.nrdc.org/sites/default/files/styles/medium_16x9_100/public/media-uploads/health4_26_airpollguide_istock_2796602_2400.jpg.jpg?h=c3635fa2&itok=bunvf3B8", 50.00m, new DateTime(2023, 12, 1, 12, 25, 0, 0, DateTimeKind.Unspecified), "Замърсяване на въздуха", new Guid("5168c799-18f9-4c11-ac0a-3f0b210e742d") },
                    { new Guid("2b95227b-0290-4f46-b05d-456980208e3a"), 2, "Замърсяването на водата унищожава нашите плажове и морета. Трябва бързо да го спрем!", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdMLiNp09BNzW4jIdGFBpJx4yDwVfeXkygeQ&usqp=CAU", 100000m, new DateTime(2023, 12, 1, 15, 5, 5, 0, DateTimeKind.Unspecified), "Замърсяване на водата", new Guid("3a9c9bea-9f47-4924-af37-5079aad72902") },
                    { new Guid("ae419d51-501b-4be5-a5ff-cf9be1781cdb"), 1, "Замърсяването е най-лошото нещо в света. Трябва да го спрем!", "https://www.interplas.com/product_images/trash-bags/sku/8-10-Gallon-Black-24-x-30-Drawstring-Trash-Bags-1000px.jpg?v=1346367336", 200.00m, new DateTime(2023, 12, 2, 10, 30, 0, 0, DateTimeKind.Unspecified), "Почистване на боклук", new Guid("4f2762fc-4689-4fbd-8e55-8e84eac060cd") }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Залесяване");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Рециклиране");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Почистване");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("25d38bb2-92c0-4c11-8025-e856c96e7652"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("2b95227b-0290-4f46-b05d-456980208e3a"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("ae419d51-501b-4be5-a5ff-cf9be1781cdb"));

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "StartDate", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("2d55face-ca9a-4015-b3f3-9ba81f0af43d"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This rubbish pollutes the environment.You need to throw it in the trash bins!", "https://www.nrdc.org/sites/default/files/styles/medium_16x9_100/public/media-uploads/health4_26_airpollguide_istock_2796602_2400.jpg.jpg?h=c3635fa2&itok=bunvf3B8", false, 50.00m, new DateTime(2023, 12, 1, 12, 25, 0, 0, DateTimeKind.Unspecified), "Air pollution", new Guid("5168c799-18f9-4c11-ac0a-3f0b210e742d") },
                    { new Guid("c4c8b110-4a84-4922-9626-c507e29d19cb"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The pollution is the worst thing ever.We need to stop it!", "https://www.interplas.com/product_images/trash-bags/sku/8-10-Gallon-Black-24-x-30-Drawstring-Trash-Bags-1000px.jpg?v=1346367336", false, 200.00m, new DateTime(2023, 12, 2, 10, 30, 0, 0, DateTimeKind.Unspecified), "Clean the rubbish", new Guid("4f2762fc-4689-4fbd-8e55-8e84eac060cd") },
                    { new Guid("ea244eaf-2677-4e62-8eed-9078056c8df6"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water pollution destroy our beaches and oceans.We need to stop it fast!", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdMLiNp09BNzW4jIdGFBpJx4yDwVfeXkygeQ&usqp=CAU", false, 100000m, new DateTime(2023, 12, 1, 15, 5, 5, 0, DateTimeKind.Unspecified), "Water pollution", new Guid("3a9c9bea-9f47-4924-af37-5079aad72902") }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "afforestation");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "recycling");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "cleaning");
        }
    }
}
