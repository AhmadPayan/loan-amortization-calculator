using backend.Dtos;
using FluentAssertions;
using UnitTests.Fixtures;

namespace UnitTests.Services;

[Collection(nameof(AmortizationCalculatorFixtureCollection))]
public class AmortizationCalculatorTest
{
    private readonly AmortizationCalculatorTestFixture _fixture;

    public AmortizationCalculatorTest(AmortizationCalculatorTestFixture fixture)
    {
        _fixture = fixture;
    }
    
    [Fact]
    public async Task CalculateAmortizationSchedule_InvalidInputProvided_ThrowsException()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _fixture.AmortizationCalculator.CalculateAmortizationSchedule(null));
    }
    
    [Fact]
    public async Task CalculateAmortizationSchedule_InvalidLoanAmount_ThrowsException()
    {
        var loanRequest = new LoanRequestDto
        {
            LoanAmount = 0
        };

        await Assert.ThrowsAsync<NullReferenceException>(() => _fixture.AmortizationCalculator.CalculateAmortizationSchedule(loanRequest));
    }
    
    [Fact]
    public async Task CalculateAmortizationSchedule_Interest_0_ShouldNotCalculateInterest()
    {
        var loanRequest = new LoanRequestDto
        {
            LoanAmount = 1000,
            InterestRate = 0,
            NumberOfPayments = 12
        };

        var payments = await _fixture.AmortizationCalculator.CalculateAmortizationSchedule(loanRequest);
        
        payments.ForEach(pmt => pmt.InterestAmount.Should().Be(0));
        payments.ForEach(pmt => pmt.PrincipalAmount.Should().BeLessThanOrEqualTo(pmt.PaymentAmount));
        var totalPrincipalSum = payments.Sum(x => x.PrincipalAmount);
        var totalPaymentSum = payments.Sum(x => x.PaymentAmount);
        totalPrincipalSum.Should().Be(totalPaymentSum);
    }
    
    [Fact]
    public async Task CalculateAmortizationSchedule_Interest_30_percentage_ShouldCalculateInterest()
    {
        var loanRequest = new LoanRequestDto
        {
            LoanAmount = 1000,
            InterestRate = 30,
            NumberOfPayments = 12
        };

        var payments = await _fixture.AmortizationCalculator.CalculateAmortizationSchedule(loanRequest);
        
        payments.ForEach(pmt => pmt.InterestAmount.Should().BeGreaterThan(0));
        payments.ForEach(pmt => pmt.PrincipalAmount.Should().BeLessThanOrEqualTo(pmt.PaymentAmount));
        var totalInterestSum = payments.Sum(x => x.InterestAmount);
        var totalPrincipalSum = payments.Sum(x => x.PrincipalAmount);
        var totalPaymentSum = payments.Sum(x => x.PaymentAmount);
        totalPaymentSum.Should().Be(totalInterestSum + totalPrincipalSum);
    }
}