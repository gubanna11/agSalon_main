using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace agSalon.Migrations
{
    public partial class attendances_null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_groups_of_services_group_id",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Services_service_id",
                table: "Attendances");

            migrationBuilder.AlterColumn<DateTime>(
                name: "time",
                table: "Attendances",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<int>(
                name: "service_id",
                table: "Attendances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "group_id",
                table: "Attendances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_groups_of_services_group_id",
                table: "Attendances",
                column: "group_id",
                principalTable: "groups_of_services",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Services_service_id",
                table: "Attendances",
                column: "service_id",
                principalTable: "Services",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_groups_of_services_group_id",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Services_service_id",
                table: "Attendances");

            migrationBuilder.AlterColumn<DateTime>(
                name: "time",
                table: "Attendances",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "service_id",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "group_id",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_groups_of_services_group_id",
                table: "Attendances",
                column: "group_id",
                principalTable: "groups_of_services",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Services_service_id",
                table: "Attendances",
                column: "service_id",
                principalTable: "Services",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
