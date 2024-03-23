﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalNewsAPI.Migrations
{
    public partial class startPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "UserDto",
                columns: table => new
                {
                    UserDtoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDto", x => x.UserDtoId);
                });
          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
 
            migrationBuilder.DropTable(
                name: "UserDto");

        }
    }
}
