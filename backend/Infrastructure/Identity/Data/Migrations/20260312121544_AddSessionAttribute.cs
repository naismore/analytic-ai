using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Identity.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recommendation_results_analytics_tools_ToolId",
                table: "recommendation_results");

            migrationBuilder.DropIndex(
                name: "IX_recommendation_results_ToolId",
                table: "recommendation_results");

            migrationBuilder.DropColumn(
                name: "Confidence",
                table: "recommendation_results");

            migrationBuilder.DropColumn(
                name: "ToolId",
                table: "recommendation_results");

            migrationBuilder.AddColumn<int>(
                name: "AnalyticsToolToolId",
                table: "recommendation_results",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToolName",
                table: "recommendation_results",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TeamSize",
                table: "recommendation_attributes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_recommendation_results_AnalyticsToolToolId",
                table: "recommendation_results",
                column: "AnalyticsToolToolId");

            migrationBuilder.AddForeignKey(
                name: "FK_recommendation_results_analytics_tools_AnalyticsToolToolId",
                table: "recommendation_results",
                column: "AnalyticsToolToolId",
                principalTable: "analytics_tools",
                principalColumn: "ToolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recommendation_results_analytics_tools_AnalyticsToolToolId",
                table: "recommendation_results");

            migrationBuilder.DropIndex(
                name: "IX_recommendation_results_AnalyticsToolToolId",
                table: "recommendation_results");

            migrationBuilder.DropColumn(
                name: "AnalyticsToolToolId",
                table: "recommendation_results");

            migrationBuilder.DropColumn(
                name: "ToolName",
                table: "recommendation_results");

            migrationBuilder.DropColumn(
                name: "TeamSize",
                table: "recommendation_attributes");

            migrationBuilder.AddColumn<double>(
                name: "Confidence",
                table: "recommendation_results",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ToolId",
                table: "recommendation_results",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_recommendation_results_ToolId",
                table: "recommendation_results",
                column: "ToolId");

            migrationBuilder.AddForeignKey(
                name: "FK_recommendation_results_analytics_tools_ToolId",
                table: "recommendation_results",
                column: "ToolId",
                principalTable: "analytics_tools",
                principalColumn: "ToolId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
