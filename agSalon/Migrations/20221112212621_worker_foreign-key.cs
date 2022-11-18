using Microsoft.EntityFrameworkCore.Migrations;

namespace agSalon.Migrations
{
    public partial class worker_foreignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
             name: "FK_Workers_Groups_Workers_worker_id",
             table: "Workers_Groups",
             column: "worker_id",
             principalTable: "Workers",
             principalColumn: "Id",
             onDelete: ReferentialAction.Cascade);


            migrationBuilder.AddForeignKey(
             name: "FK_Attendances_Workers_worker_id",
             table: "Attendances",
             column: "worker_id",
             principalTable: "Workers",
             principalColumn: "Id",
             onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
