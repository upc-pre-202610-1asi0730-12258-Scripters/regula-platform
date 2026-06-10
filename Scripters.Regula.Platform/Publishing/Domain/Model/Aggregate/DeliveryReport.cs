namespace Scripters.Regula.Platform.Publishing.Domain.Model.Aggregate;

public class DeliveryReport
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string Status { get; set; } = string.Empty;

    public int CylindersDelivered { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime ScheduledDate { get; set; }

    public int? DeliveryDurationMinutes { get; set; }
}