using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bankingLibrary;

namespace www.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private static readonly Operation BankSystem = new Operation(); 

    public static BankAccount? Account { get; set; }

    [BindProperty]
    public string operation { get; set; }

    [BindProperty]
    public decimal amount { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
      
    }

    public void OnPostOpenAccount()
    {
      Account = BankSystem.OpenBankAccount();
    }

    public void OnPostExecuteOperation()
    {
      switch(operation)
      {
        case "D":
          Account = BankSystem.DoDeposit(Account.Code, amount);
          break;
        case "R":
          Account = BankSystem.DoWithdraw(Account.Code, amount);
          break;
      }
      amount = 0;
    }
}
