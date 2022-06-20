﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieStore.WebApi.Migrations
{
    public partial class addStatus_ForMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Movies");
        }
    }
}
