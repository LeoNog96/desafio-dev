using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cnab.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transaction_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    kind = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    signal = table.Column<char>(type: "character(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    removed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    removed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    value = table.Column<double>(type: "double precision", nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    card = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    uploaded_by = table.Column<Guid>(type: "uuid", nullable: false),
                    store_owner = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    store_name = table.Column<string>(type: "character varying(19)", maxLength: 19, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    removed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    removed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.id);
                    table.ForeignKey(
                        name: "FK_transactions_transaction_types_type",
                        column: x => x.type,
                        principalTable: "transaction_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_transactions_users_uploaded_by",
                        column: x => x.uploaded_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_transaction_types_kind",
                table: "transaction_types",
                column: "kind");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_card",
                table: "transactions",
                column: "card");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_cpf",
                table: "transactions",
                column: "cpf");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_date",
                table: "transactions",
                column: "date");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_store_name",
                table: "transactions",
                column: "store_name");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_store_owner",
                table: "transactions",
                column: "store_owner");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_type",
                table: "transactions",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_uploaded_by",
                table: "transactions",
                column: "uploaded_by");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_uploaded_by_date",
                table: "transactions",
                columns: new[] { "uploaded_by", "date" });

            migrationBuilder.CreateIndex(
                name: "IX_transactions_uploaded_by_date_cpf",
                table: "transactions",
                columns: new[] { "uploaded_by", "date", "cpf" });

            migrationBuilder.CreateIndex(
                name: "IX_transactions_uploaded_by_date_cpf_type",
                table: "transactions",
                columns: new[] { "uploaded_by", "date", "cpf", "type" });

            migrationBuilder.CreateIndex(
                name: "IX_users_login",
                table: "users",
                column: "login");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "transaction_types");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
