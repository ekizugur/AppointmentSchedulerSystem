using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSchedular.Data.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationmg5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: new Guid("99e4d7bc-5565-434b-9219-0dbde7b932df"));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "AppointmentDate",
                table: "Appointments",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3e137a10-7527-4fca-ae19-3aecb5c46278"),
                column: "ConcurrencyStamp",
                value: "1b0c8c60-9f82-4302-8985-db266f23c0dc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6286c136-92fa-4d66-b8d8-0b3ab4bbe33d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a00d247-9b11-4bab-94ae-1e69aca2a4ed", "AQAAAAEAACcQAAAAEOvWs/O9N8eVDPNqLE3RCLPlAQYU8BbHwedbqXIaX0lpBx1gf50oH9CilyNf4Ebwcw==", "bcea65f9-283e-4a53-81dd-67a4d336f013" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDate",
                table: "Appointments",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDate", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name", "UserId" },
                values: new object[] { new Guid("99e4d7bc-5565-434b-9219-0dbde7b932df"), new DateTime(2023, 2, 1, 19, 15, 0, 0, DateTimeKind.Unspecified), "Admin Deneme", new DateTime(2023, 1, 23, 23, 24, 58, 507, DateTimeKind.Local).AddTicks(8538), null, null, false, null, null, "Diş Hekimliği Randevusu 1 2 3 4 5 6 Deneme123", new Guid("6286c136-92fa-4d66-b8d8-0b3ab4bbe33d") });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3e137a10-7527-4fca-ae19-3aecb5c46278"),
                column: "ConcurrencyStamp",
                value: "faa2f7b4-a947-453e-84f9-7e7c5c1cc38a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6286c136-92fa-4d66-b8d8-0b3ab4bbe33d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b48cde60-fa51-4ad4-813e-9e0c87c7cd71", "AQAAAAEAACcQAAAAEF+TcDadg98i0j+9ryE1S+8MdTqtxr5rHRaTXjq6RIYSNYTVDjWIBj82sIAVWWLvvQ==", "30efc908-e932-480a-b649-7857bcd8f9ba" });
        }
    }
}
