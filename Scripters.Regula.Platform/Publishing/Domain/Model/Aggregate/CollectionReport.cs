namespace Scripters.Regula.Platform.Publishing.Domain.Model.Aggregate;

public class CollectionReport
{
    public int Id { get; set; }

    public int CompanyId { get; set; }
    
    public string ClientName { get; set; } = string.Empty;

    public decimal DebtAmount { get; set; }

    public decimal PaidAmount { get; set; }

    public DateTime DebtDate { get; set; }

    public string Status { get; set; } = string.Empty;
}