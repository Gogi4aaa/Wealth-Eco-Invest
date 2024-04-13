using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wealth_Eco_Invest.Data.Migrations
{
    public partial class addChatTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("0beeec07-5341-48fd-8ae6-6b53fa41f4bb"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("644668ec-75bd-4a57-994c-8a011942666b"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("8e9524f2-1dd3-493a-9d99-b5c0d27fd838"));

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserFrom = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserTo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnnounceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_Announces_AnnounceId",
                        column: x => x.AnnounceId,
                        principalTable: "Announces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("0337a67d-275c-4098-a4a1-eff3be03ec7a"), 1, "The pollution is the worst thing ever.We need to stop it!", "https://www.interplas.com/product_images/trash-bags/sku/8-10-Gallon-Black-24-x-30-Drawstring-Trash-Bags-1000px.jpg?v=1346367336", 200.00m, new DateTime(2023, 12, 2, 10, 30, 0, 0, DateTimeKind.Unspecified), "Clean the rubbish", new Guid("4f2762fc-4689-4fbd-8e55-8e84eac060cd") });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("5d20597e-5b54-48ec-b485-ed032ef6ad94"), 3, "This rubbish pollutes the environment.You need to throw it in the trash bins!", "https://www.nrdc.org/sites/default/files/styles/medium_16x9_100/public/media-uploads/health4_26_airpollguide_istock_2796602_2400.jpg.jpg?h=c3635fa2&itok=bunvf3B8", 50.00m, new DateTime(2023, 12, 1, 12, 25, 0, 0, DateTimeKind.Unspecified), "Air pollution", new Guid("5168c799-18f9-4c11-ac0a-3f0b210e742d") });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("b438aaae-b8ed-4186-80c2-a50efdfec165"), 2, "Water pollution destroy our beaches and oceans.We need to stop it fast!", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdMLiNp09BNzW4jIdGFBpJx4yDwVfeXkygeQ&usqp=CAU", 100000m, new DateTime(2023, 12, 1, 15, 5, 5, 0, DateTimeKind.Unspecified), "Water pollution", new Guid("3a9c9bea-9f47-4924-af37-5079aad72902") });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_AnnounceId",
                table: "Chats",
                column: "AnnounceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("0337a67d-275c-4098-a4a1-eff3be03ec7a"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("5d20597e-5b54-48ec-b485-ed032ef6ad94"));

            migrationBuilder.DeleteData(
                table: "Announces",
                keyColumn: "Id",
                keyValue: new Guid("b438aaae-b8ed-4186-80c2-a50efdfec165"));

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("0beeec07-5341-48fd-8ae6-6b53fa41f4bb"), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water pollution destroy our beaches and oceans.We need to stop it fast!", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdMLiNp09BNzW4jIdGFBpJx4yDwVfeXkygeQ&usqp=CAU", false, 100000m, new DateTime(2023, 12, 1, 15, 5, 5, 0, DateTimeKind.Unspecified), "Water pollution", new Guid("3a9c9bea-9f47-4924-af37-5079aad72902") });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("644668ec-75bd-4a57-994c-8a011942666b"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The pollution is the worst thing ever.We need to stop it!", "https://www.interplas.com/product_images/trash-bags/sku/8-10-Gallon-Black-24-x-30-Drawstring-Trash-Bags-1000px.jpg?v=1346367336", false, 200.00m, new DateTime(2023, 12, 2, 10, 30, 0, 0, DateTimeKind.Unspecified), "Clean the rubbish", new Guid("4f2762fc-4689-4fbd-8e55-8e84eac060cd") });

            migrationBuilder.InsertData(
                table: "Announces",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Price", "StartDate", "Title", "UserId" },
                values: new object[] { new Guid("8e9524f2-1dd3-493a-9d99-b5c0d27fd838"), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This rubbish pollutes the environment.You need to throw it in the trash bins!", "https://www.nrdc.org/sites/default/files/styles/medium_16x9_100/public/media-uploads/health4_26_airpollguide_istock_2796602_2400.jpg.jpg?h=c3635fa2&itok=bunvf3B8", false, 50.00m, new DateTime(2023, 12, 1, 12, 25, 0, 0, DateTimeKind.Unspecified), "Air pollution", new Guid("5168c799-18f9-4c11-ac0a-3f0b210e742d") });
        }
    }
}
