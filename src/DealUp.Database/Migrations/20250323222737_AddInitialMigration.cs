using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealUp.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvertisementCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SellerProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellerProfile_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPendingConfirmation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPendingConfirmation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPendingConfirmation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advertisement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Tags = table.Column<string>(type: "text", nullable: false),
                    Location_Latitude = table.Column<decimal>(type: "numeric", nullable: false),
                    Location_Longitude = table.Column<decimal>(type: "numeric", nullable: false),
                    Statistics_ChatCount = table.Column<int>(type: "integer", nullable: false),
                    Statistics_FavoriteCount = table.Column<int>(type: "integer", nullable: false),
                    Statistics_ViewCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisement_AdvertisementCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AdvertisementCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Advertisement_SellerProfile_SellerId",
                        column: x => x.SellerId,
                        principalTable: "SellerProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementPhoto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: false),
                    AdvertisementId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertisementPhoto_Advertisement_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Labels = table.Column<string>(type: "text", nullable: false),
                    AdvertisementId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Advertisement_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_CategoryId",
                table: "Advertisement",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_SellerId",
                table: "Advertisement",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementPhoto_AdvertisementId",
                table: "AdvertisementPhoto",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_AdvertisementId",
                table: "Product",
                column: "AdvertisementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SellerProfile_UserId",
                table: "SellerProfile",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPendingConfirmation_UserId",
                table: "UserPendingConfirmation",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertisementPhoto");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "UserPendingConfirmation");

            migrationBuilder.DropTable(
                name: "Advertisement");

            migrationBuilder.DropTable(
                name: "AdvertisementCategory");

            migrationBuilder.DropTable(
                name: "SellerProfile");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
