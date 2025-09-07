using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentAPlace.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIsReadByReceiverToMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReadByReceiver",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReplyAt",
                table: "Messages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReplyText",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReadByReceiver",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ReplyAt",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ReplyText",
                table: "Messages");
        }
    }
}
