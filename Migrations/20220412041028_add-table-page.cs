using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ogani.Migrations
{
    public partial class addtablepage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0f73c4bc-be3e-4cb9-a4c7-733194fcaf4c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e65a252-71b9-4224-b7d0-1c3cc0b76c35"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c554b60e-0cf6-449d-97b1-ccfb87e73b61"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("cd8fdb3b-3d1a-4cc1-b8d2-6113fccf4d24"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f0dc148d-f577-4e20-bb08-d30e94ce795d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f7a3b03f-e3e8-4fa8-9a71-1b29bd1ddc07"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a00d6584-fc5c-44ab-8e2f-b4e36e1e58a6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2161eccc-e4fe-4f96-a200-ed8566084a99"));

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalView = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("3e6cc4d7-acb2-4fb8-af93-deb867de4d8b"), "", "Meat" },
                    { new Guid("d73a9902-a13c-46e4-aeea-5a934b969cf0"), "", "Oranges" },
                    { new Guid("634d7e0b-50d7-4fc3-ba2c-841b48538264"), "", "Fastfood" },
                    { new Guid("3804ea55-834f-43b8-954e-2a05603a57b5"), "", "Fresh Bananas" },
                    { new Guid("b7bda22d-7613-4588-a8e8-f29d90cbf716"), "", "Drink Fruits" },
                    { new Guid("894d0da7-269b-46ab-8190-6f7854f0e581"), "", "Sea Food" }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "Name", "TotalView" },
                values: new object[] { new Guid("3d5a6cad-25cd-4aef-9d16-3f22d6d5d717"), "Home Page", 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ed040235-219c-48d8-a12d-3ae4d89a2fb9"),
                column: "CreateAt",
                value: new DateTime(2022, 4, 12, 11, 10, 26, 898, DateTimeKind.Local).AddTicks(4505));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreateAt", "CurrentPrice", "Description", "Image", "Name", "Rate", "ReducePrice", "SupplierId", "ToTalRemaining" },
                values: new object[] { new Guid("2a25175e-7e8a-4b45-bc4f-6c502469ac77"), new DateTime(2022, 4, 12, 11, 10, 26, 900, DateTimeKind.Local).AddTicks(3003), "500", "Feature", "/img/featured/feature-2.jpg", "Feature-2", 0, "200", new Guid("ab77aefb-5a93-4fa6-abfb-5c904d7ad5b8"), 5 });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cc88ab6f-5d66-4c30-a60e-8f5254f1e112"),
                column: "ConcurrencyStamp",
                value: "d3ffe1a0-7cc7-442e-87af-224f144e5ca7");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("f79a59cf-9295-401f-a1cb-bc861a6977ad"), "e078ea04-2217-40ef-9d0c-c630781759f8", "employee", "Employee" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0027068e-4c5d-4ecb-a157-b9cc063cd672"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "975d7642-cd97-4521-a223-f292b8f72e3e", "AQAAAAEAACcQAAAAEOBhpkbHDwufzzuflu/HPZQNNThc3rhX8PksbI69av+ST6kw2iSknZyTBIkHbzxTLg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3804ea55-834f-43b8-954e-2a05603a57b5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3e6cc4d7-acb2-4fb8-af93-deb867de4d8b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("634d7e0b-50d7-4fc3-ba2c-841b48538264"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("894d0da7-269b-46ab-8190-6f7854f0e581"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b7bda22d-7613-4588-a8e8-f29d90cbf716"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d73a9902-a13c-46e4-aeea-5a934b969cf0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2a25175e-7e8a-4b45-bc4f-6c502469ac77"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f79a59cf-9295-401f-a1cb-bc861a6977ad"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("c554b60e-0cf6-449d-97b1-ccfb87e73b61"), "", "Meat" },
                    { new Guid("0f73c4bc-be3e-4cb9-a4c7-733194fcaf4c"), "", "Oranges" },
                    { new Guid("f7a3b03f-e3e8-4fa8-9a71-1b29bd1ddc07"), "", "Fastfood" },
                    { new Guid("cd8fdb3b-3d1a-4cc1-b8d2-6113fccf4d24"), "", "Fresh Bananas" },
                    { new Guid("9e65a252-71b9-4224-b7d0-1c3cc0b76c35"), "", "Drink Fruits" },
                    { new Guid("f0dc148d-f577-4e20-bb08-d30e94ce795d"), "", "Sea Food" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ed040235-219c-48d8-a12d-3ae4d89a2fb9"),
                column: "CreateAt",
                value: new DateTime(2022, 4, 4, 0, 33, 23, 156, DateTimeKind.Local).AddTicks(5518));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreateAt", "CurrentPrice", "Description", "Image", "Name", "Rate", "ReducePrice", "SupplierId", "ToTalRemaining" },
                values: new object[] { new Guid("a00d6584-fc5c-44ab-8e2f-b4e36e1e58a6"), new DateTime(2022, 4, 4, 0, 33, 23, 158, DateTimeKind.Local).AddTicks(9563), "500", "Feature", "/img/featured/feature-2.jpg", "Feature-2", 0, "200", new Guid("ab77aefb-5a93-4fa6-abfb-5c904d7ad5b8"), 5 });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cc88ab6f-5d66-4c30-a60e-8f5254f1e112"),
                column: "ConcurrencyStamp",
                value: "e2c26aa7-f848-47df-b1f5-e78ca73084bb");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("2161eccc-e4fe-4f96-a200-ed8566084a99"), "ea6b833f-b1b6-439a-aa91-f0a24e8d33e6", "employee", "Employee" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0027068e-4c5d-4ecb-a157-b9cc063cd672"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c3d41948-7493-4fa3-bb02-4587f1efad10", "AQAAAAEAACcQAAAAEKOyzvOjACVFmpPRwH0oGZZBGUYUjMnmom42yOcnUDyRJM2bWrKysInxjyfUExSa2A==" });
        }
    }
}
