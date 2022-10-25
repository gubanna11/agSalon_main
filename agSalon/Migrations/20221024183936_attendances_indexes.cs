using Microsoft.EntityFrameworkCore.Migrations;

namespace agSalon.Migrations
{
    public partial class attendances_indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_client_id_date_time",
                table: "Attendances",
                columns: new[] { "client_id", "date", "time" });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_worker_id_date_time",
                table: "Attendances",
                columns: new[] { "worker_id", "date", "time" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Attendances_client_id_date_time",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_worker_id_date_time",
                table: "Attendances");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_client_id",
                table: "Attendances",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_worker_id",
                table: "Attendances",
                column: "worker_id");
        }
    }
}
