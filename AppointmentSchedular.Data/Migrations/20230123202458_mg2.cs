using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSchedular.Data.Migrations
{
    /// <inheritdoc />
    public partial class mg2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: new Guid("08173894-b774-4a64-8abd-ab31a14faa71"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDate",
                table: "Appointments",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: new Guid("99e4d7bc-5565-434b-9219-0dbde7b932df"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDate",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDate", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name", "UserId" },
                values: new object[] { new Guid("08173894-b774-4a64-8abd-ab31a14faa71"), new DateTime(2023, 2, 1, 19, 15, 0, 0, DateTimeKind.Unspecified), "Admin Deneme", new DateTime(2023, 1, 23, 22, 51, 55, 456, DateTimeKind.Local).AddTicks(4160), null, null, false, null, null, "Diş Hekimliği Randevusu 1 2 3 4 5 6 Deneme123", new Guid("6286c136-92fa-4d66-b8d8-0b3ab4bbe33d") });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3e137a10-7527-4fca-ae19-3aecb5c46278"),
                column: "ConcurrencyStamp",
                value: "bbbe659b-3ca2-4229-963e-2e217610d197");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6286c136-92fa-4d66-b8d8-0b3ab4bbe33d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "436e703a-938d-462c-9d09-3cf56a4fad41", "AQAAAAEAACcQAAAAEIbM8bDIea+2KtAoVBGmraCDrqq81uBeEAiFnc8am+DsAGtMKo0fmy05QLQkGYejag==", "ce932bf6-4a7c-4c58-b615-51dcd0c31363" });
        }
    }
}
