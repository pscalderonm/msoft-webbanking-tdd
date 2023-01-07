namespace projectTest;

public class WhenExecOperationShould
{
  
  [Fact]
  public void SucceedWhenOpeningAccount()
  {  
    //Arrange
    const decimal expectedInitialBalance = 100;
    //Act
    var result = new bankingLibrary.Operation().OpenBankAccount(initialBalance:expectedInitialBalance);
    //Assert
    Assert.NotNull(result);
    Assert.Equal(expectedInitialBalance, result.Balance); 
  }

  [Fact]
  public void SucceedWhenMakingADeposit()
  {
    //Arrange
    const decimal expectedFinalBalance=200;
    var operation = new bankingLibrary.Operation();
    var account = operation.OpenBankAccount(initialBalance:100);

    //Act
    account = operation.DoDeposit(account.Code!, 100);

    //Assert
    Assert.Equal(expectedFinalBalance, account.Balance);
  }

  [Fact]
  public void SucceedWhenMakingAWithdraw()
  {
    //Arrange
    const decimal expectedFinalBalance=100;
    var operation = new bankingLibrary.Operation();
    var account = operation.OpenBankAccount(initialBalance:200);

    //Act
    account = operation.DoWithdraw(account.Code!, 100);

    //Assert
    Assert.Equal(expectedFinalBalance, account.Balance);
  }

  [Fact]
  public void FailWhenOverdraftArise()
  {
    //Arrange
    var operation = new bankingLibrary.Operation();
    var account = operation.OpenBankAccount(initialBalance:200);

    //Act
    var act = () => operation.DoWithdraw(account.Code!, 1000);

    //Assert
    Assert.Throws<bankingLibrary.OverdraftNotAllowedException>(act);
  }

  [Fact]
  public void SucceedWhenRequestingCurrentAccountBalance()
  {
    //Arrange
    var operation = new bankingLibrary.Operation();
    var account = operation.OpenBankAccount(initialBalance:200);
    var expectedFinalBalance = 200;
    
    //Act 
    var balance = operation.GetFinalBalance(account.Code!);

    //Assert
    Assert.Equal(expectedFinalBalance, balance);
  }

  [Fact]
  public void SucceedWhenRequestingAccountMovementCount()
  {
    //Arrange
    var operation = new bankingLibrary.Operation();
    var account = operation.OpenBankAccount();
    operation.DoDeposit(account.Code!, 100);
    operation.DoWithdraw(account.Code!, 100);
    var expectedMovementCount = 2; 

    //Act
    var movementCount = operation.GetMovementCount(account.Code!);

    //Assert
    Assert.Equal(expectedMovementCount, movementCount);
  }

  [Fact]
  public void SucceedWhenPayingDailyInterestUponAccountBalance()
  {
    throw new NotImplementedException();
  }

  [Fact]
  public void SucceedWhenAllowingOverdraft()
  {
    //Arrange
    var operation = new bankingLibrary.Operation();
    var account = operation.OpenBankAccount(initialBalance:100, isOverdraftAllowed:true);
    var expectedFinalBalance = -100;

    //Act
    operation.DoWithdraw(account.Code!, 200);
    var balance = operation.GetFinalBalance(account.Code!);

    //Assert
    Assert.Equal(expectedFinalBalance, balance);
  }

  [Fact]
  public void SucceedWhenCheckingValidAccountStatus()
  {
    //Arrange
    var operation = new bankingLibrary.Operation();
    var account = operation.OpenBankAccount();

    //Act
    var isAccountValid = operation.IsValidAccount(account.Code!);

    //Assert
    Assert.True(isAccountValid);
  }

  [Fact]
  public void FailWhenMakingZeroDeposit()
  {
    //Arrange
    var expectedFinalBalance = 100M;

    var operation = new bankingLibrary.Operation();
    var account = operation.OpenBankAccount(initialBalance:expectedFinalBalance);

    //Act
    var act = ()=>operation.DoDeposit(account.Code!, 0);
    var act2 = ()=>operation.GetMovementCount(account.Code!);
    
    //Assert
    Assert.Throws<bankingLibrary.InvalidTransactionAmountException>(act);
    Assert.Equal(0, act2());
  }

  [Fact]
  public void FailWhenMakingZeroWithdraw()
  {
    //Arrange
    var expectedFinalBalance = 100M;

    var operation = new bankingLibrary.Operation();
    var account = operation.OpenBankAccount(initialBalance:expectedFinalBalance);

    //Act
    var act = ()=>operation.DoWithdraw(account.Code!, 0);
    var act2= ()=>operation.GetMovementCount(account.Code!);
    
    //Assert
    Assert.Throws<bankingLibrary.InvalidTransactionAmountException>(act);
    Assert.Equal(0, act2());
  }
}