using backend.Dtos;
using backend.Models;

namespace backend.Services.Contracts;

public interface IAmortizationCalculator
{
    Task<List<PaymentSchedule>> CalculateAmortizationSchedule(LoanRequestDto loan);
}