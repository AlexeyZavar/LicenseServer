namespace LicenseServer.Database.Entities;

public class Event
{
    public Guid Id { get; set; }

    public string Text { get; set; }
    public EventType Type { get; set; }

    public string IP { get; set; }
    public DateTime Date { get; set; }

    public Guid? LicenseId { get; set; }
    public License? License { get; set; }
    public Guid HardwareId { get; set; }
}

public enum EventType
{
    Information = 1,
    Exception = 2
}
