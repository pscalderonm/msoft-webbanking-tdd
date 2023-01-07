namespace bankingLibrary;

public enum BankAccountStates
{
  Opened,
  Closed,
  Suspended
}


public class BankAccount
{
  public string? Code {get; set;}
  public decimal Balance {get; set;}
  public BankAccountStates State {get; set;}
  public IEnumerable<AccountMovement> Movements{get; set;}
  public bool IsOverdraftAllowed { get; set;}

  public BankAccount()
  {
    Movements = new List<AccountMovement>();
  }
}