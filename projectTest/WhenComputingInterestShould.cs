namespace projectTest;
public class WhenComputingInterestShould
{

  [Fact]
  public void SucceedIfPositiveAmount()
  {
    //Arrange
    var amount = 100M;
    var rate = .2M;
    var expectedResult = 0.05M;
    
    //Act
    var result = new bankingLibrary.Interest().Compute(amount, rate);

    //Assert
    Assert.Equal(expectedResult, result, 2); 
  }

  [Fact]
  public void BeZeroIfAmountZero()
  {
    //Arrange
    var amount = 0;
    var rate = .2M;
    var expectedResult = 0;

    //Act
    var result = new bankingLibrary.Interest().Compute(amount, rate);

    //Asert
    Assert.Equal(expectedResult, result, 2);
  }  

  [Fact]
  public void BeZeroIfNegativeAmount()
  {
    //Arrange
    var amount = -100;
    var rate = .2M;
    var expectedResult = 0;

    //Act
    var result = new bankingLibrary.Interest().Compute(amount, rate);

    //Asert
    Assert.Equal(expectedResult, result, 2);
  }  
}