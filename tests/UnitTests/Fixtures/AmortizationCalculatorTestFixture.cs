using backend.Services;

namespace UnitTests.Fixtures;

public class AmortizationCalculatorTestFixture
{
    public readonly AmortizationCalculator AmortizationCalculator;

    public AmortizationCalculatorTestFixture()
    {
        AmortizationCalculator = new AmortizationCalculator();
    }
}