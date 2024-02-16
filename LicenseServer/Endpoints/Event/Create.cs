#region

using FastEndpoints;
using LicenseServer.Authentication;
using LicenseServer.Core.Services;
using LicenseServer.Database.Entities;

#endregion

namespace LicenseServer.Endpoints.Event;

public class Create : Endpoint<CreateEventRequest>
{
    private readonly EventService _eventService;

    /// <inheritdoc />
    public Create(EventService eventService)
    {
        _eventService = eventService;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Post("/event");
        AuthSchemes(XAuthSchemeConstants.SchemeName);
        Description(x => x
                         .Produces(204)
                         .ProducesProblem(401));
    }

    /// <inheritdoc />
    public override async Task HandleAsync(CreateEventRequest req, CancellationToken ct)
    {
        var @event = new Database.Entities.Event
        {
            Text = req.Text,
            Type = req.Type,
            IP = GetIP(),
            LicenseId = req.LicenseId,
            HardwareId = req.HardwareId,
            Date = DateTime.UtcNow
        };

        await _eventService.Create(@event);
    }

    public string GetIP()
    {
        if (!string.IsNullOrEmpty(HttpContext.Request.Headers["CF-CONNECTING-IP"]))
        {
            return HttpContext.Request.Headers["CF-CONNECTING-IP"];
        }

        var ipAddress = HttpContext.GetServerVariable("HTTP_X_FORWARDED_FOR");

        if (!string.IsNullOrEmpty(ipAddress))
        {
            var addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses.Last();
            }
        }

        return HttpContext.Connection.RemoteIpAddress!.ToString();
    }
}

public class CreateEventRequest
{
    [FromClaim(XClaimTypes.Id)] public Guid LicenseId { get; set; }
    public string Text { get; set; }
    public EventType Type { get; set; }
    public Guid HardwareId { get; set; }
}
