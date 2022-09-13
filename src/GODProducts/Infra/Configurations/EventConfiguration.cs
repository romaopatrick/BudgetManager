using GODCommon.Contexts.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace GODProducts.Infra.Configurations;

public class EventConfiguration: BaseEntityConfiguration<Event>
{
    public override void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.Property(x => x.Snapshot)
            .HasConversion(
                v => JsonConvert.SerializeObject(v,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<Product>(v,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }) ?? new());
        
        base.Configure(builder);
    }
}