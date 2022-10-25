﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace agSalon.Migrations
{
    public partial class attendances_indexesupdated2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Attendances_client_id_date_time",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_worker_id_date_time",
                table: "Attendances");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_client_id_date_service_id",
                table: "Attendances",
                columns: new[] { "client_id", "date", "service_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_worker_id_date_service_id",
                table: "Attendances",
                columns: new[] { "worker_id", "date", "service_id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Attendances_client_id_date_service_id",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_worker_id_date_service_id",
                table: "Attendances");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_client_id_date_time",
                table: "Attendances",
                columns: new[] { "client_id", "date", "time" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_worker_id_date_time",
                table: "Attendances",
                columns: new[] { "worker_id", "date", "time" },
                unique: true);
        }
    }
}
