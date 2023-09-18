using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

/// <summary>
/// Provided loan info by user
/// </summary>
public class LoanRequestDto
{
    /// <summary>
    /// Amount of the loan
    /// </summary>
    [Display(Name = "Loan Amount")]
    public decimal LoanAmount { get; set; }
    /// <summary>
    /// The annual rate of the loan in percentage (APR).
    /// </summary>
    [Display(Name = "Interest % (APR)")]
    public double InterestRate { get; set; }
    /// <summary>
    /// The total number of payments in order to pay off the given loan amount.
    /// </summary>
    [Display(Name = "Number of Payments")]
    public int NumberOfPayments { get; set; }
    
    /// <summary>
    /// Payment frequency. Daily, Weekly, Monthly, Yearly
    /// </summary>
    [Display(Name = "Payment Frequency")]
    public string PaymentFrequency { get; set; }
}