using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class skillseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "ID", "Damage", "Name" },
                values: new object[,]
                {
                    { 1, 30, "FireBall" },
                    { 2, 20, "Frenzy" },
                    { 3, 50, "Blizzard" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
