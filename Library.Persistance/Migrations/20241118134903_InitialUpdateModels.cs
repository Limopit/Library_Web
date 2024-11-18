using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialUpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_authors_author_id",
                table: "books");

            migrationBuilder.RenameColumn(
                name: "imageUrls",
                table: "books",
                newName: "ImageUrls");

            migrationBuilder.RenameColumn(
                name: "author_id",
                table: "books",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_books_author_id",
                table: "books",
                newName: "IX_books_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_books_authors_AuthorId",
                table: "books",
                column: "AuthorId",
                principalTable: "authors",
                principalColumn: "author_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_authors_AuthorId",
                table: "books");

            migrationBuilder.RenameColumn(
                name: "ImageUrls",
                table: "books",
                newName: "imageUrls");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "books",
                newName: "author_id");

            migrationBuilder.RenameIndex(
                name: "IX_books_AuthorId",
                table: "books",
                newName: "IX_books_author_id");

            migrationBuilder.AddForeignKey(
                name: "FK_books_authors_author_id",
                table: "books",
                column: "author_id",
                principalTable: "authors",
                principalColumn: "author_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
