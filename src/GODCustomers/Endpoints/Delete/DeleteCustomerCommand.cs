namespace GODCustomers.Endpoints.Delete;

public sealed class DeleteCustomerCommand
{
    public Guid CustomerId { get; init; }
}