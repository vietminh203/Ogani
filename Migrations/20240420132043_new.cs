using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ogani.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { new Guid("9649bc5a-0502-41fa-8062-25a65c7a356c"), "", "Meat" },
                    { new Guid("31884358-d0f9-4145-bee3-4057677dbd25"), "", "Oranges" },
                    { new Guid("95442d70-4906-4780-9f60-324d0287ddd9"), "", "Fastfood" },
                    { new Guid("d61fae88-aaae-4a91-96cd-46a7197c89f1"), "", "Fresh Bananas" },
                    { new Guid("5b954ab2-1b66-4f65-9fdc-6832e67f3737"), "", "Drink Fruits" },
                    { new Guid("763fd466-3868-4e75-9711-188939c1bf6f"), "", "Sea Food" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ed040235-219c-48d8-a12d-3ae4d89a2fb9"),
                column: "CreateAt",
                value: new DateTime(2024, 4, 20, 20, 20, 43, 340, DateTimeKind.Local).AddTicks(2596));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreateAt", "CurrentPrice", "Description", "Image", "Name", "Rate", "ReducePrice", "SupplierId", "ToTalRemaining" },
                values: new object[] { new Guid("8297152f-92f9-4bdf-91a4-1c02b12ad045"), new DateTime(2024, 4, 20, 20, 20, 43, 341, DateTimeKind.Local).AddTicks(7451), "500", "Feature", "/img/featured/feature-2.jpg", "Feature-2", 0, "200", new Guid("ab77aefb-5a93-4fa6-abfb-5c904d7ad5b8"), 5 });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cc88ab6f-5d66-4c30-a60e-8f5254f1e112"),
                column: "ConcurrencyStamp",
                value: "a739e147-91dc-4b5f-8dfb-432308bf5d54");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("646ef9d0-dba9-40a0-a1fe-6d5e26021f22"), "caa712d6-a76e-4993-a84b-457a07b45303", "employee", "Employee" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0027068e-4c5d-4ecb-a157-b9cc063cd672"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0106d8f3-8f22-41fd-9024-74b4d6ada1b9", "AQAAAAEAACcQAAAAED3YF28fYmqKXBNO1zxDAY7CViIkW6dtufUJS2auWVEVuB2C+KX8iHV0m5qtPtp/Rw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("31884358-d0f9-4145-bee3-4057677dbd25"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5b954ab2-1b66-4f65-9fdc-6832e67f3737"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("763fd466-3868-4e75-9711-188939c1bf6f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("95442d70-4906-4780-9f60-324d0287ddd9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9649bc5a-0502-41fa-8062-25a65c7a356c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d61fae88-aaae-4a91-96cd-46a7197c89f1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8297152f-92f9-4bdf-91a4-1c02b12ad045"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("646ef9d0-dba9-40a0-a1fe-6d5e26021f22"));

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
    }
}
