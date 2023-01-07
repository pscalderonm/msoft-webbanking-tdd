namespace bankingLibrary;
public class Interest
{
  public decimal Compute(decimal amount, decimal rate)=>
   amount > 0 ? (amount*rate)/365 : 0;
}
