using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace agSalon.Migrations
{
    /// <inheritdoc />
    public partial class groupimg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "img_url",
                table: "groups_of_services",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_groups_of_services_name",
                table: "groups_of_services",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_groups_of_services_name",
                table: "groups_of_services");

            migrationBuilder.DropColumn(
                name: "img_url",
                table: "groups_of_services");
        }
    }
}
