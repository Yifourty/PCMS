
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PCMS.Domain.Entities;

namespace PCMS.Infrastructure.Data.Configuration
{
    public class LeagueFormatConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasKey(tl => new { tl.Id });
        }
    }
}
