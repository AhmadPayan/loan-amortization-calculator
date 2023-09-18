namespace backend.Models;

/// <summary>
/// Payment amounts
/// </summary>
public class PaymentSchedule
{
    /// <summary>
    /// Payment amount is amount of: Principal + Interest 
    /// </summary>
    public decimal PaymentAmount { get; set; }
    /// <summary>
    /// The principal amount is payment minus interest
    /// </summary>
    public decimal PrincipalAmount { get; set; }
    /// <summary>
    /// Amount of interest
    /// </summary>
    public decimal InterestAmount { get; set; }
    /// <summary>
    /// Total remaining amount
    /// </summary>
    public decimal RemainingBalance { get; set; }
}