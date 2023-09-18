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
}