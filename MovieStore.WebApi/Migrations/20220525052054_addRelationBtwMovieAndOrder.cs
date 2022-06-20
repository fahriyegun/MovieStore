using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieStore.WebApi.Migrations
{
    public partial class addRelationBtwMovieAndOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Orders_OrdersId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_OrdersId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "OrdersId",
                table: "Movies");

            migrationBuilder.AlterColumn<int>(
                name: "DirectorId",
                table: "Movies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "MoviesOrders",
                columns: table => new
                {
                    MoviesId = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesOrders", x => new { x.MoviesId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_MoviesOrders_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviesOrders_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviesOrders_OrdersId",
                table: "MoviesOrders",
                column: "OrdersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviesOrders");

            migrationBuilder.AlterColumn<int>(
                name: "DirectorId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrdersId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_OrdersId",
                table: "Movies",
                column: "OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Orders_OrdersId",
                table: "Movies",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
