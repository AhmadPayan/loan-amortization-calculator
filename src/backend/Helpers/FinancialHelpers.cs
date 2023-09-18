namespace backend.Helpers;

public static class FinancialHelpers
{
    public static decimal CalculateMonthlyPayment(decimal loanAmount, decimal monthlyInterestRate, int numberOfPayments)
    {
        if (monthlyInterestRate <= 0)
            return loanAmount / numberOfPayments;
        
        var power = (decimal)Math.Pow(1 + (double)monthlyInterestRate, numberOfPayments);
        return loanAmount * (monthlyInterestRate * power) / (power - 1);
    }
}