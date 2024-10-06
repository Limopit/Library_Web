using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    author_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    author_firstname = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    author_lastname = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    author_birthday = table.Column<DateOnly>(type: "TEXT", maxLength: 32, nullable: true),
                    author_country = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authors", x => x.author_id);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    book_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    book_ISBN = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    book_name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    book_genre = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    book_descr = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    author_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    book_issue_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    book_issue_expiration_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.book_id);
                    table.ForeignKey(
                        name: "FK_books_authors_author_id",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "author_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_books_author_id",
                table: "books",
                column: "author_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "authors");
        }
    }
}
