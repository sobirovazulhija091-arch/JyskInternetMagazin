public class Discount// (Скидки)
{
   public int  Id{get;set;}
 
   public string Code{get;set;}=null!;

   public int Percentage{get;set;}

   public DateTime ExpiryDate{get;set;}//Срок действия до

   public bool IsActive{get;set;}
}


