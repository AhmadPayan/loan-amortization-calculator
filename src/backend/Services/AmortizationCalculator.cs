using backend.Dtos;
using backend.Helpers;
using backend.Models;
using backend.Services.Contracts;

namespace backend.Services;

public class AmortizationCalculator : IAmortizationCalculator
{
    public async Task<List<PaymentSchedule>> CalculateAmortizationSchedule(LoanRequestDto loan)
    {
        if (loan is null) throw new NullReferenceException($"Parameter {nameof(loan)} cannot be NULL");
        
        if (loan.LoanAmount <=0) throw new NullReferenceException($"Invalid loan amount! The loan amount cannot be less than or equal to 0");

        var amortizationSchedule = new List<PaymentSchedule>();
        var monthlyInterestRate = (decimal)(loan.InterestRate / 12.0 / 100.0);
        var monthlyPayment = Math.Round(FinancialHelpers.CalculateMonthlyPayment(loan.LoanAmount, monthlyInterestRate, loan.NumberOfPayments), 2);

        var remainingBalance = loan.LoanAmount;

        for (var number = 1; number <= loan.NumberOfPayments; number++)
        {
            var interestPayment = Math.Round(remainingBalance * monthlyInterestRate, 2);
            var principalPayment = monthlyPayment - interestPayment;

            remainingBalance -= principalPayment;
            if (remainingBalance < principalPayment)
            {
                principalPayment += remainingBalance;
                monthlyPayment += remainingBalance;
                remainingBalance = 0;
            }

            var payment = new PaymentSchedule
            {
                PaymentFrequencyNumber = number,
                PrincipalAmount = principalPayment,
                InterestAmount = interestPayment,
                PaymentAmount = monthlyPayment,
                RemainingBalance = remainingBalance
            };

            amortizationSchedule.Add(payment);
        }

        return amortizationSchedule;
    }
}