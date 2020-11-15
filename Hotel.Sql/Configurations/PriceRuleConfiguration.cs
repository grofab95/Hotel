using Hotel.Domain.Entities.PriceRuleEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hotel.Sql.Configurations
{
    class PriceRuleConfiguration : IEntityTypeConfiguration<PriceRule>
    {
        public void Configure(EntityTypeBuilder<PriceRule> builder)
        {
            builder.ToTable("PriceRules");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getdate()");

            builder.Property(x => x.RuleName)
                .HasConversion(new EnumToStringConverter<RuleName>());

            builder.Property(x => x.RuleType)
                .HasConversion(new EnumToStringConverter<RuleType>());
        }
    }
}
