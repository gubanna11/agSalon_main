using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace agSalon.Migrations
{
    public partial class attendances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    client_id = table.Column<int>(type: "int", nullable: false),
                    group_id = table.Column<int>(type: "int", nullable: false),
                    service_id = table.Column<int>(type: "int", nullable: false),
                    worker_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    price = table.Column<double>(type: "double", nullable: false),
                    rendered = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Attendances_Clients_client_id",
                        column: x => x.client_id,
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendances_groups_of_services_group_id",
                        column: x => x.group_id,
                        principalTable: "groups_of_services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendances_Services_service_id",
                        column: x => x.service_id,
                        principalTable: "Services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendances_Workers_worker_id",
                        column: x => x.worker_id,
                        principalTable: "Workers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_client_id",
                table: "Attendances",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_group_id",
                table: "Attendances",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_service_id",
                table: "Attendances",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_worker_id",
                table: "Attendances",
                column: "worker_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");
        }
    }
}
