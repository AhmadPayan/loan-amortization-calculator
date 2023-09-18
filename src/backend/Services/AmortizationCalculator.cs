using backend.Dtos;
using backend.Models;
using backend.Services.Contracts;

namespace backend.Services;

public class AmortizationCalculator : IAmortizationCalculator
{
    public Task<List<PaymentSchedule>> CalculateAmortizationSchedule(LoanRequestDto loan)
    {
        if (loan is null) throw new NullReferenceException($"Parameter {nameof(loan)} cannot be NULL");
        
        return null;
    }
}