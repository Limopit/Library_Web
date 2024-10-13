using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialUpdateBR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "book_issue_date",
                table: "books");

            migrationBuilder.DropColumn(
                name: "book_issue_expiration_date",
                table: "books");

            migrationBuilder.CreateTable(
                name: "BorrowRecords",
                columns: table => new
                {
                    record_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    book_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    user_id = table.Column<string>(type: "TEXT", nullable: false),
                    book_issue_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    book_issue_expiration_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowRecords", x => x.record_id);
                    table.ForeignKey(
                        name: "FK_BorrowRecords_AspNetUsers_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BorrowRecords_books_book_id",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRecords_book_id",
                table: "BorrowRecords",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRecords_user_id",
                table: "BorrowRecords",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowRecords");

            migrationBuilder.AddColumn<DateTime>(
                name: "book_issue_date",
                table: "books",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "book_issue_expiration_date",
                table: "books",
                type: "TEXT",
                nullable: true);
        }
    }
}
