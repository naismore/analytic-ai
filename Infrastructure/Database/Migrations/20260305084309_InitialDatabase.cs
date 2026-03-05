using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "analytics_tools",
                columns: table => new
                {
                    ToolId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    ComplexityLevel = table.Column<int>(type: "integer", nullable: false),
                    MaxDataVolume = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_analytics_tools", x => x.ToolId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "conversations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_conversations_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RevokedReason = table.Column<string>(type: "text", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_profiles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SkillLevel = table.Column<int>(type: "integer", nullable: false),
                    DataVolume = table.Column<int>(type: "integer", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_profiles", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_user_profiles_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_messages_conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recommendation_sessions",
                columns: table => new
                {
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uuid", nullable: true),
                    TaskDescription = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recommendation_sessions", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_recommendation_sessions_conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_recommendation_sessions_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "recommendation_results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ToolId = table.Column<int>(type: "integer", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false),
                    Confidence = table.Column<double>(type: "double precision", nullable: false),
                    ReasoningSummary = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recommendation_results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recommendation_results_analytics_tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "analytics_tools",
                        principalColumn: "ToolId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_recommendation_results_recommendation_sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "recommendation_sessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_conversations_UserId",
                table: "conversations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_ConversationId_CreatedAt",
                table: "messages",
                columns: new[] { "ConversationId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_recommendation_results_SessionId_Rank",
                table: "recommendation_results",
                columns: new[] { "SessionId", "Rank" });

            migrationBuilder.CreateIndex(
                name: "IX_recommendation_results_ToolId",
                table: "recommendation_results",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_recommendation_sessions_ConversationId",
                table: "recommendation_sessions",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_recommendation_sessions_UserId",
                table: "recommendation_sessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "recommendation_results");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "user_profiles");

            migrationBuilder.DropTable(
                name: "analytics_tools");

            migrationBuilder.DropTable(
                name: "recommendation_sessions");

            migrationBuilder.DropTable(
                name: "conversations");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
