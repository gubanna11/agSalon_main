using Microsoft.EntityFrameworkCore.Migrations;

namespace agSalon.Migrations
{
    public partial class workers_groups2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worker_Group_groups_of_services_group_id",
                table: "Worker_Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Worker_Group_Workers_worker_id",
                table: "Worker_Group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Worker_Group",
                table: "Worker_Group");

            migrationBuilder.RenameTable(
                name: "Worker_Group",
                newName: "Workers_Groups");

            migrationBuilder.RenameIndex(
                name: "IX_Worker_Group_group_id",
                table: "Workers_Groups",
                newName: "IX_Workers_Groups_group_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workers_Groups",
                table: "Workers_Groups",
                columns: new[] { "worker_id", "group_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Groups_groups_of_services_group_id",
                table: "Workers_Groups",
                column: "group_id",
                principalTable: "groups_of_services",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Groups_Workers_worker_id",
                table: "Workers_Groups",
                column: "worker_id",
                principalTable: "Workers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Groups_groups_of_services_group_id",
                table: "Workers_Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Groups_Workers_worker_id",
                table: "Workers_Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workers_Groups",
                table: "Workers_Groups");

            migrationBuilder.RenameTable(
                name: "Workers_Groups",
                newName: "Worker_Group");

            migrationBuilder.RenameIndex(
                name: "IX_Workers_Groups_group_id",
                table: "Worker_Group",
                newName: "IX_Worker_Group_group_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Worker_Group",
                table: "Worker_Group",
                columns: new[] { "worker_id", "group_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_Group_groups_of_services_group_id",
                table: "Worker_Group",
                column: "group_id",
                principalTable: "groups_of_services",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_Group_Workers_worker_id",
                table: "Worker_Group",
                column: "worker_id",
                principalTable: "Workers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
