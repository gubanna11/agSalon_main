using Microsoft.EntityFrameworkCore.Migrations;

namespace agSalon.Migrations
{
    public partial class workers_groups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Worker_Group",
                columns: table => new
                {
                    worker_id = table.Column<int>(type: "int", nullable: false),
                    group_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker_Group", x => new { x.worker_id, x.group_id });
                    table.ForeignKey(
                        name: "FK_Worker_Group_groups_of_services_group_id",
                        column: x => x.group_id,
                        principalTable: "groups_of_services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Worker_Group_Workers_worker_id",
                        column: x => x.worker_id,
                        principalTable: "Workers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Group_group_id",
                table: "Worker_Group",
                column: "group_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Worker_Group");
        }
    }
}
