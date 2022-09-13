using GODCommon.Contexts.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace GODOrders.Infra.Configurations;

public sealed class EventConfiguration : BaseEntityConfiguration<Event>
{
    public override void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.Property(x => x.Snapshot)
            .HasConversion(
                v => JsonConvert.SerializeObject(v,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<Order>(v,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }) ?? new());
        
        base.Configure(builder);
    }
}