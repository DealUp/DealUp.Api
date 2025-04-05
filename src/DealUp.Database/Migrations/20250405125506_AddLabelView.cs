using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealUp.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddLabelView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                create view "View_UniqueLabel" as
                select distinct on ("Name", "Value") "Name", "ValueType", "Value"
                from "Label";
                """
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                drop view "View_UniqueLabel";
                """
                );
        }
    }
}
