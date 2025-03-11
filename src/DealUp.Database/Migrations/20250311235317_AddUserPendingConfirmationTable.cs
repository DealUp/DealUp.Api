using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealUp.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPendingConfirmationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "Users",
                newName: "IX_Users_Username");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Users",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "Unverified");

            migrationBuilder.CreateTable(
                name: "UserPendingConfirmations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPendingConfirmations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPendingConfirmations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPendingConfirmations_UserId",
                table: "UserPendingConfirmations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPendingConfirmations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Username",
                table: "Users",
                newName: "IX_Users_Email");
        }
    }
}
