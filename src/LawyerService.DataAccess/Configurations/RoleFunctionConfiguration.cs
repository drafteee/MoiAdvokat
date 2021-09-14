using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LawyerService.Entities.Lawyer;
using LawyerService.Entities.Identity;

namespace LawyerService.DataAccess.Configurations
{
    public class RoleFunctionConfiguration : IEntityTypeConfiguration<RoleFunction>
    {
        public void Configure(EntityTypeBuilder<RoleFunction> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(x => x.Role).WithMany(x => x.RoleFunctions).HasForeignKey(x=>x.RoleId);
            builder.HasOne(x => x.Function).WithMany(x => x.RoleFunctions).HasForeignKey(x=>x.FunctionId);
        }
    }
}
