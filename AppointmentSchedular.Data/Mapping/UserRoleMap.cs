using AppointmentSchedular.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedular.Data.Mapping
{
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            builder.HasData(
            new AppUserRole
            {
                UserId = Guid.Parse("6286C136-92FA-4D66-B8D8-0B3AB4BBE33D"),
                RoleId = Guid.Parse("3E137A10-7527-4FCA-AE19-3AECB5C46278")
            });
        }
    }
}
