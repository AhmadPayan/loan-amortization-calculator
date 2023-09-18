using backend.Dtos;
using backend.Models;
using backend.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace backend.Pages;

[ValidateAntiForgeryToken]
public class IndexModel : PageModel
{
    private readonly IAmortizationCalculator _amortizationCalculator;

    public IndexModel(IAmortizationCalculator amortizationCalculator)
    {
        _amortizationCalculator = amortizationCalculator;
    }

    [BindProperty] public LoanRequestDto? Loan { get; set; }
    public IEnumerable<PaymentSchedule>? AmortizationSchedule { get; set; }

    public void OnGet()
    {
        SetPaymentFrequencies();
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            AmortizationSchedule = await _amortizationCalculator.CalculateAmortizationSchedule(Loan);
        }

        SetPaymentFrequencies();
        return Page();
    }

    private void SetPaymentFrequencies()
    {
        var paymentFrequencies = new List<SelectListItem>()
        {
            new()
            {
                Text = "Monthly", Value = "1", Selected = true
            }
        };

        ViewData["PaymentFrequencyData"] = paymentFrequencies;
    }
}