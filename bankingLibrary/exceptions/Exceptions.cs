namespace bankingLibrary;

public class BankAccountNotFoundException:Exception{

  public BankAccountNotFoundException()
    :base(){}
}

public class OverdraftNotAllowed:Exception{
  public OverdraftNotAllowed()
  :base(){}
}