namespace bankingLibrary;

public class BankAccountNotFoundException:Exception{

  public BankAccountNotFoundException()
    :base(){}
}

public class OverdraftNotAllowedException:Exception{
  public OverdraftNotAllowedException()
  :base(){}
}

public class InvalidTransactionAmountException:Exception{
  public InvalidTransactionAmountException()
  :base(){}
}