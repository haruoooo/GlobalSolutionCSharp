using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureWork.Data.Context.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LEARNING_PATH",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Title = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(400)", maxLength: 400, nullable: true),
                    Area = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEARNING_PATH", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PROFESSIONAL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROFESSIONAL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MODULE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    LearningPathId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    WorkloadHours = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MODULE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MODULE_LEARNINGPATH",
                        column: x => x.LearningPathId,
                        principalTable: "LEARNING_PATH",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PROGRESS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ProfessionalId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ModuleId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Percentage = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 0),
                    UpdatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROGRESS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PROGRESS_MODULE",
                        column: x => x.ModuleId,
                        principalTable: "MODULE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PROGRESS_PROFESSIONAL",
                        column: x => x.ProfessionalId,
                        principalTable: "PROFESSIONAL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MODULE_LearningPathId",
                table: "MODULE",
                column: "LearningPathId");

            migrationBuilder.CreateIndex(
                name: "IX_PROGRESS_ModuleId",
                table: "PROGRESS",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_PROGRESS_ProfessionalId",
                table: "PROGRESS",
                column: "ProfessionalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PROGRESS");

            migrationBuilder.DropTable(
                name: "MODULE");

            migrationBuilder.DropTable(
                name: "PROFESSIONAL");

            migrationBuilder.DropTable(
                name: "LEARNING_PATH");
        }
    }
}
