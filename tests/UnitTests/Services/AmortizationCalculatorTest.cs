using backend.Dtos;
using backend.Services;

namespace UnitTests.Services;

public class AmortizationCalculatorTest
{
    [Fact]
    public async Task CalculateAmortizationSchedule_InvalidInputProvided_ThrowsException()
    {
        var sut = new AmortizationCalculator();

        await Assert.ThrowsAsync<NullReferenceException>(() => sut.CalculateAmortizationSchedule(null));
    }
    
    [Fact]
    public async Task CalculateAmortizationSchedule_InvalidLoanAmount_ThrowsException()
    {
        var sut = new AmortizationCalculator();
        var loanRequest = new LoanRequestDto
        {
            LoanAmount = 0
        };

        await Assert.ThrowsAsync<NullReferenceException>(() => sut.CalculateAmortizationSchedule(loanRequest));
    }
}