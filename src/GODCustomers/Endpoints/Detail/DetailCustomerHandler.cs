using System.Net;
using GODCommon.Notifications;
using GODCommon.Results;
using GODCustomers.Infra;
using Microsoft.EntityFrameworkCore;

namespace GODCustomers.Endpoints.Detail;

public class DetailCustomerHandler : BaseHandler<DetailCustomerCommand, Customer>
{
    public DetailCustomerHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<Customer>> Handle(DetailCustomerCommand req, CancellationToken ct)
    {
        var customer = await Context.Customers.FirstOrDefaultAsync(b => b.SnapshotNumber == req.SnapshotNumber, ct);
        if (customer is null) return RFac.WithError<Customer>
            (CustomerNotifications.CustomerNotFound, HttpStatusCode.NotFound);

        return RFac.WithSuccess(customer);
    }
}