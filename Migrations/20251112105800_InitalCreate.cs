using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HighPerformanceFraudDetectionSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "FraudRules",
                columns: table => new
                {
                    FraudRuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConditionExpression = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FraudRules", x => x.FraudRuleId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FraudCases",
                columns: table => new
                {
                    FraudCaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    FraudRuleId = table.Column<int>(type: "int", nullable: false),
                    DetectedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FraudCases", x => x.FraudCaseId);
                    table.ForeignKey(
                        name: "FK_FraudCases_FraudRules_FraudRuleId",
                        column: x => x.FraudRuleId,
                        principalTable: "FraudRules",
                        principalColumn: "FraudRuleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FraudCases_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FraudRuleTransaction",
                columns: table => new
                {
                    FraudRulesFraudRuleId = table.Column<int>(type: "int", nullable: false),
                    TransactionsTransactionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FraudRuleTransaction", x => new { x.FraudRulesFraudRuleId, x.TransactionsTransactionId });
                    table.ForeignKey(
                        name: "FK_FraudRuleTransaction_FraudRules_FraudRulesFraudRuleId",
                        column: x => x.FraudRulesFraudRuleId,
                        principalTable: "FraudRules",
                        principalColumn: "FraudRuleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FraudRuleTransaction_Transactions_TransactionsTransactionId",
                        column: x => x.TransactionsTransactionId,
                        principalTable: "Transactions",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionFraudRules",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    FraudRuleId = table.Column<int>(type: "int", nullable: false),
                    ConfidenceScore = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DetectedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionFraudRules", x => new { x.TransactionId, x.FraudRuleId });
                    table.ForeignKey(
                        name: "FK_TransactionFraudRules_FraudRules_FraudRuleId",
                        column: x => x.FraudRuleId,
                        principalTable: "FraudRules",
                        principalColumn: "FraudRuleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionFraudRules_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FraudCases_FraudRuleId",
                table: "FraudCases",
                column: "FraudRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_FraudCases_TransactionId",
                table: "FraudCases",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_FraudRuleTransaction_TransactionsTransactionId",
                table: "FraudRuleTransaction",
                column: "TransactionsTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionFraudRules_FraudRuleId",
                table: "TransactionFraudRules",
                column: "FraudRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FraudCases");

            migrationBuilder.DropTable(
                name: "FraudRuleTransaction");

            migrationBuilder.DropTable(
                name: "TransactionFraudRules");

            migrationBuilder.DropTable(
                name: "FraudRules");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
