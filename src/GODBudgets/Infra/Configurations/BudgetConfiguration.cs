using GODCommon.Contexts.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GODBudgets.Infra.Configurations;

public sealed class BudgetConfiguration : BaseEntityConfiguration<Budget>.AsSnapshotConfiguration<Budget>
{
}