namespace backend.Dtos;

/// <summary>
/// Provided loan info by user
/// </summary>
public class LoanRequestDto
{
    /// <summary>
    /// Amount of the loan
    /// </summary>
    public decimal LoanAmount { get; set; }
    /// <summary>
    /// The annual rate of the loan in percentage (APR).
    /// </summary>
    public double InterestRate { get; set; }
    /// <summary>
    /// The total number of payments in order to pay off the given loan amount.
    /// </summary>
    public int NumberOfPayments { get; set; }
}