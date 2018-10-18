using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RAMHX.CMS.WebCore.Data.Migrations
{
    public partial class AppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
       name: "LockoutEndDateUtc",
       table: "AspNetUsers",
       type: "DateTime",
       nullable: true); migrationBuilder.AddColumn<string>(
        name: "FirstName",
        table: "AspNetUsers",
        type: "nvarchar(450)",
        nullable: true); migrationBuilder.AddColumn<string>(
        name: "LastName",
        table: "AspNetUsers",
        type: "nvarchar(450)",
        nullable: true); migrationBuilder.AddColumn<string>(
        name: "Address",
        table: "AspNetUsers",
        type: "nvarchar(450)",
        nullable: true);
        migrationBuilder.AddColumn<string>(
        name: "City",
        table: "AspNetUsers",
        type: "nvarchar(450)",
        nullable: true);
        migrationBuilder.AddColumn<string>(
        name: "Mobile",
        table: "AspNetUsers",
        type: "nvarchar(20)",
        nullable: true);
        migrationBuilder.AddColumn<string>(
        name: "Gender",
        table: "AspNetUsers",
        type: "nvarchar(20)",
        nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
