using System.Net;

public class Order
{
    public int Id{get;set;}
    public string UserId{get;set;}
    public User User{get;set;}=null!;
    public DateTime OrderDate{get;set;}=DateTime.UtcNow;
    public string status{get;set;}=null!;//=EnumStatue.Paid;loke this you can set default value for enum
    public decimal TotalAmount{get;set;}
    public List<OrderItem> OrderItems{get;set;}=[];
}