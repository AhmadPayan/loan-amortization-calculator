using backend.Dtos;
using backend.Services;
using FluentAssertions;

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
    
    [Fact]
    public async Task CalculateAmortizationSchedule_Interest_0_ShouldNotCalculateInterest()
    {
        var sut = new AmortizationCalculator();
        var loanRequest = new LoanRequestDto
        {
            LoanAmount = 1000,
            InterestRate = 0,
            NumberOfPayments = 12
        };

        var payments = await sut.CalculateAmortizationSchedule(loanRequest);
        
        payments.ForEach(pmt => pmt.InterestAmount.Should().Be(0));
        payments.ForEach(pmt => pmt.PrincipalAmount.Should().BeLessThanOrEqualTo(pmt.PaymentAmount));
        var totalPrincipalSum = payments.Sum(x => x.PrincipalAmount);
        var totalPaymentSum = payments.Sum(x => x.PaymentAmount);
        totalPrincipalSum.Should().Be(totalPaymentSum);
    }
    
    [Fact]
    public async Task CalculateAmortizationSchedule_Interest_30_percentage_ShouldCalculateInterest()
    {
        var sut = new AmortizationCalculator();
        var loanRequest = new LoanRequestDto
        {
            LoanAmount = 1000,
            InterestRate = 30,
            NumberOfPayments = 12
        };

        var payments = await sut.CalculateAmortizationSchedule(loanRequest);
        
        payments.ForEach(pmt => pmt.InterestAmount.Should().BeGreaterThan(0));
        payments.ForEach(pmt => pmt.PrincipalAmount.Should().BeLessThanOrEqualTo(pmt.PaymentAmount));
        var totalInterestSum = payments.Sum(x => x.InterestAmount);
        var totalPrincipalSum = payments.Sum(x => x.PrincipalAmount);
        var totalPaymentSum = payments.Sum(x => x.PaymentAmount);
        totalPaymentSum.Should().Be(totalInterestSum + totalPrincipalSum);
    }
}