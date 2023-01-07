namespace bankingLibrary;

public enum MovementTypes
{
  Deposit,
  Withdraw
}

public class AccountMovement
{
  public string? Description {get; set;}
  public MovementTypes MovementType {get; set;}
  public decimal Amount {get; set;}
}