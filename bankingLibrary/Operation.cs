namespace bankingLibrary;

public class Operation
{
  private readonly Dictionary<string, BankAccount> _repo =
  new Dictionary<string, BankAccount>();

  public BankAccount OpenBankAccount(decimal initialBalance=0, bool isOverdraftAllowed=false)
  {
    var account = new BankAccount{
      Code=Guid.NewGuid().ToString(), 
      Balance=initialBalance,
      State = BankAccountStates.Opened,
      IsOverdraftAllowed = isOverdraftAllowed
    };
    _repo.Add(account.Code,account);
    return account;
  }

  public BankAccount DoDeposit(string accountCode, decimal amount)
  {
    if(!_repo.TryGetValue(accountCode, out var bankAccount))
    {
      throw new BankAccountNotFoundException();
    }

    if(amount <= 0)
    {
      throw new InvalidTransactionAmountException();
    }

    bankAccount.Balance += amount;
    bankAccount.Movements = bankAccount.Movements.Append(new AccountMovement{
      MovementType = MovementTypes.Deposit,
      Amount = amount
    });
    _repo[accountCode] = bankAccount;

    return bankAccount;
  }

  public BankAccount DoWithdraw(string accountCode, decimal amount)
  {
    if(!_repo.TryGetValue(accountCode, out var bankAccount))
    {
      throw new BankAccountNotFoundException();
    }

     if(amount <= 0)
    {
      throw new InvalidTransactionAmountException();
    }

    if(!bankAccount.IsOverdraftAllowed && bankAccount.Balance < amount)
    {
      throw new OverdraftNotAllowedException();
    }

    bankAccount.Balance -= amount;
    bankAccount.Movements = bankAccount.Movements.Append(new AccountMovement{
      MovementType = MovementTypes.Withdraw,
      Amount = amount
    });
    _repo[accountCode] = bankAccount;


    return bankAccount;
  }

  public decimal GetFinalBalance(string accountCode)
  {
    if(!_repo.TryGetValue(accountCode, out var bankAccount))
    {
      throw new BankAccountNotFoundException();
    }

    return bankAccount.Balance;
  }

  public int GetMovementCount(string accountCode)
  {
    if(!_repo.TryGetValue(accountCode, out var bankAccount))
    {
      throw new BankAccountNotFoundException();
    }
    
    return bankAccount.Movements.Count(); 
  }

  public bool IsValidAccount(string accountCode)
  {
    if(!_repo.TryGetValue(accountCode, out var bankAccount))
    {
      throw new BankAccountNotFoundException();
    }
    return bankAccount.State == BankAccountStates.Opened;
  }

  public void PayInterest(string accountCode, decimal rate)
  {
    var balance = GetFinalBalance(accountCode);
    var annualInterest = new Interest().Compute(balance, rate);
    DoDeposit(accountCode, annualInterest);
  }
}