namespace Serilog.Basic;

public class Payment
{
    public int PaymentType { get; set; }
    public decimal Amount { get; set; }
    public DateTime OccuredAt { get; set; }
}