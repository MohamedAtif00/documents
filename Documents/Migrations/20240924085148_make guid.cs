using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Documents.Migrations
{
    /// <inheritdoc />
    public partial class makeguid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Documents_DocumentId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_DocumentId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId1",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "FilePath", "Name", "SignedDate", "Status" },
                values: new object[,]
                {
                    { new Guid("0e4a27cc-1034-4b78-b460-aad0499229ad"), "/docs/Document2.pdf", "Document2.pdf", null, "Signed" },
                    { new Guid("3d638ace-fc8f-453d-b298-06f6d9ba3bcf"), "/docs/Document3.pdf", "Document3.pdf", null, "Hold" },
                    { new Guid("4ff07ef7-0ed9-4b24-9637-c70d59bc9f2a"), "/docs/Document1.pdf", "Document1.pdf", null, "Reviewed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_DocumentId1",
                table: "Comments",
                column: "DocumentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Documents_DocumentId1",
                table: "Comments",
                column: "DocumentId1",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Documents_DocumentId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_DocumentId1",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("0e4a27cc-1034-4b78-b460-aad0499229ad"));

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("3d638ace-fc8f-453d-b298-06f6d9ba3bcf"));

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("4ff07ef7-0ed9-4b24-9637-c70d59bc9f2a"));

            migrationBuilder.DropColumn(
                name: "DocumentId1",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Documents",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "FilePath", "Name", "SignedDate", "Status" },
                values: new object[,]
                {
                    { 1, "/docs/Document1.pdf", "Document1.pdf", null, "Reviewed" },
                    { 2, "/docs/Document2.pdf", "Document2.pdf", null, "Signed" },
                    { 3, "/docs/Document3.pdf", "Document3.pdf", null, "Hold" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_DocumentId",
                table: "Comments",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Documents_DocumentId",
                table: "Comments",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
