﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EagerLoading.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Tokat" },
                    { 2, "Corum" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Name", "RegionId", "Salary", "Surname" },
                values: new object[,]
                {
                    { 1, "Dincer", 1, 8000, "Yigit" },
                    { 2, "Suna", 2, 5000, "Yigit" },
                    { 3, "Dilara", 1, 100, "Yigit" },
                    { 4, "Erdinc", 2, 10000, "Yigit" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "EmployeeId", "OrderDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 2, 5, 15, 39, 3, 122, DateTimeKind.Local).AddTicks(9651) },
                    { 2, 1, new DateTime(2024, 2, 5, 15, 39, 3, 122, DateTimeKind.Local).AddTicks(9662) },
                    { 3, 2, new DateTime(2024, 2, 5, 15, 39, 3, 122, DateTimeKind.Local).AddTicks(9662) },
                    { 4, 2, new DateTime(2024, 2, 5, 15, 39, 3, 122, DateTimeKind.Local).AddTicks(9663) },
                    { 5, 3, new DateTime(2024, 2, 5, 15, 39, 3, 122, DateTimeKind.Local).AddTicks(9664) },
                    { 6, 3, new DateTime(2024, 2, 5, 15, 39, 3, 122, DateTimeKind.Local).AddTicks(9665) },
                    { 7, 3, new DateTime(2024, 2, 5, 15, 39, 3, 122, DateTimeKind.Local).AddTicks(9668) },
                    { 8, 4, new DateTime(2024, 2, 5, 15, 39, 3, 122, DateTimeKind.Local).AddTicks(9669) },
                    { 9, 4, new DateTime(2024, 2, 5, 15, 39, 3, 122, DateTimeKind.Local).AddTicks(9670) },
                    { 10, 1, new DateTime(2024, 2, 5, 15, 39, 3, 122, DateTimeKind.Local).AddTicks(9671) },
                    { 11, 2, new DateTime(2024, 2, 5, 15, 39, 3, 122, DateTimeKind.Local).AddTicks(9673) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RegionId",
                table: "Employees",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
