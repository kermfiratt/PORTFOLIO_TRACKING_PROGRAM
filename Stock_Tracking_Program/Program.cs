using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace Stock_Market_Tracking_System_By_Kerem_Hamza_FIRAT

{



class Stocks 
{
    public string Stock_Short_Code { get; set; }
    public double Cost { get; set; }
    public int Amount { get; set; }
    public DateTime Date {get; set; }

}


class AddTransaction
{
  public string Stock_Short_Code { get; set; }

  public int Add_Amount { get; set; }

  public double Add_Cost { get; set; }

  public DateTime Date { get; set; }
}

class SellTransaction
{
    public string Stock_Short_Code { get; set; }
    public int Sold_Amount { get; set; }
    public double Sold_Cost { get; set; }
    public DateTime Date { get; set; }
}




class Program

{
    static List<Stocks> stock = new List<Stocks>();

    static List<AddTransaction> addTransactions = new List<AddTransaction>();

    static List<SellTransaction> sellTransactions = new List<SellTransaction>();
    
    static void Main(string[]args) 
    
    {
      while (true)
      {
        
        Console.WriteLine("WELCOME TO STOCK_TRACKING_SYSTEM");
        
        Console.WriteLine("1. Add New Stock");
        
        Console.WriteLine("2. Sell Stock");
        
        Console.WriteLine("3. See Transactions");
        
        Console.WriteLine("4. Total Value");

        int choice;

        if(int.TryParse(Console.ReadLine(), out choice))
        {

          switch(choice)
          {

              case 1: 
              Add_Stock();
              break;

              case 2: 
              Sell_Stock();
              break;

              case 3:
              See_Transactions();
              break;

              case 4: 
              Total_Value();
              break;

              default:
              Console.WriteLine("Invalid Choice, Try Again.");
              break;
          }
        }
      
      }
      


    }

    
    static void Add_Stock()              // Yeni bir hisse eklemek icin kullanilan method. (TURKISH)
                                         // Method for adding new stocks.                  (ENGLISH)
    {
        Console.Write("Enter stock short name: ");
        string stock_to_add = Console.ReadLine();

        Console.Write("Enter the cost: ");
        double cost = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the amount: ");
        int amount = Convert.ToInt32(Console.ReadLine());

        var currentStock = stock.Find(s => s.Stock_Short_Code == stock_to_add);
        
            addTransactions.Add (new AddTransaction    
        
            {Stock_Short_Code = stock_to_add,

            Add_Cost = cost,

            Add_Amount = amount,
           
            Date = DateTime.Now
            
           });

           stock.Add(new Stocks
           
           {Stock_Short_Code = stock_to_add,

            Cost = cost,

            Amount = amount,
           
            Date = DateTime.Now});

            Console.WriteLine($"{amount} {stock_to_add} Succesfully Added.");
    }

   
   
   static void Sell_Stock()                                // Bir hissnin satisi icin kullanilan method. (TURKISH)
{                                                          // Method for selling a stock.                (ENGLISH)
    Console.Write("Enter stock short name to sell: ");
    string stock_To_Sell = Console.ReadLine();

    Console.Write("Enter sell stock cost: ");
    double cost_of_sell_stock = Convert.ToDouble(Console.ReadLine());

    var currentStock = stock.Find(s => s.Stock_Short_Code == stock_To_Sell);

    if (currentStock != null)
    {
        Console.Write("Enter the amount to sell: ");
        int sellAmount = Convert.ToInt32(Console.ReadLine());

        if (sellAmount <= currentStock.Amount)
        {
            currentStock.Amount -= sellAmount;

            
            sellTransactions.Add(new SellTransaction
            {
                Stock_Short_Code = stock_To_Sell,
                Sold_Amount = sellAmount,
                Sold_Cost = cost_of_sell_stock,
                Date = DateTime.Now
            });

            Console.WriteLine($"{sellAmount} amount of {stock_To_Sell} sold with {cost_of_sell_stock} ₺ cost.");
        }
        else
        {
            Console.WriteLine("Inavlid stock amount.");
        }
    }
    else
    {
        Console.WriteLine("Stock not found.");
    }
}

    
     static void See_Transactions()                            // Gecmiste yapilan islemleri goruntulemeye yarayan method.
{                                                              // Method for cheching the past transactions.
    if (addTransactions.Count == 0 && sellTransactions.Count == 0)
    {
        Console.WriteLine("No transactions yet.");
        
        return;
    }

    Console.WriteLine("Transaction History: ");

    foreach (var addTransaction in addTransactions)
    {
        Console.WriteLine($"ADD Stock: {addTransaction.Stock_Short_Code} / Amount: {addTransaction.Add_Amount} / Cost: {addTransaction.Add_Cost} / Date: {addTransaction.Date}");
    }

    foreach (var sellTransaction in sellTransactions)
    {
        Console.WriteLine($"SOLD Stock: {sellTransaction.Stock_Short_Code} / Amount: {sellTransaction.Sold_Amount} / Cost: {sellTransaction.Sold_Cost} / Date: {sellTransaction.Date}");
    }
}

     

     
static void Total_Value()
{
    if (stock.Count == 0)
    {
        Console.WriteLine("Total value cannot be calculated because there are no stocks.");
        return;
    }

    Dictionary<string, int> totalShares = new Dictionary<string, int>();
    Dictionary<string, double> totalValue = new Dictionary<string, double>();
    Dictionary<string, double> averageCost = new Dictionary<string, double>();
    double totalPortfolioValue = 0;

    foreach (var item in stock)
    {
        if (totalShares.ContainsKey(item.Stock_Short_Code))
        {
            totalShares[item.Stock_Short_Code] += item.Amount; 
            totalValue[item.Stock_Short_Code] += item.Amount * item.Cost; 
        }
        else
        {
            totalShares[item.Stock_Short_Code] = item.Amount; 
            totalValue[item.Stock_Short_Code] = item.Amount * item.Cost; 
        }

        totalPortfolioValue += item.Amount * item.Cost; 
    }

    Console.WriteLine("Total Shares, Total Value, and Average Cost for each stock:");
    foreach (var kvp in totalShares)
    {
        string stockName = kvp.Key;
        int shares = kvp.Value;
        double value = totalValue[stockName];
        double avgCost = value / shares;

        Console.WriteLine($"Stock: {stockName} / Total Shares: {shares} / Total Value: {value} / Average Cost: {avgCost}");
    }

    double totalAverageCost = totalPortfolioValue / stock.Sum(s => s.Amount);
    Console.WriteLine($"Total Portfolio Value: {totalPortfolioValue}");
}


}

}