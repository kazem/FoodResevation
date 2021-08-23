using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddSecurityStamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SecurityStamp",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationRegister",
                table: "RequestFoodReservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 26, 42, 924, DateTimeKind.Local).AddTicks(3303),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 6, 17, 23, 7, 13, 454, DateTimeKind.Local).AddTicks(50));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "PeriodDefinitions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 13, 0, 26, 42, 900, DateTimeKind.Local).AddTicks(7120),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 6, 17, 23, 7, 13, 433, DateTimeKind.Local).AddTicks(9078));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationRegister",
                table: "RequestFoodReservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 17, 23, 7, 13, 454, DateTimeKind.Local).AddTicks(50),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 26, 42, 924, DateTimeKind.Local).AddTicks(3303));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "PeriodDefinitions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 17, 23, 7, 13, 433, DateTimeKind.Local).AddTicks(9078),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 13, 0, 26, 42, 900, DateTimeKind.Local).AddTicks(7120));
        }
    }
}
