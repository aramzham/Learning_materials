using Destructurama.Attributed;

namespace Serilog.Basic;

public class Payment
{
    public Guid Id { get; set; }
    public int PaymentType { get; set; }
    public decimal Amount { get; set; }
    public DateTime OccuredAt { get; set; }
    [NotLogged]
    public string? Description { get; set; }
    [LogMasked(ShowFirst = 3, PreserveLength = true)]
    public string Email { get; set; }
    [LogMasked(Text = "_MASKED_")]
    public string CardNumber { get; set; }
}