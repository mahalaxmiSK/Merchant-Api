using ConsoleTables;
using Merchant.BL;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Orders
            await GetOrders();

            //Top 5 products
            GetTop5Products();

            //Update stock
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }

        }
        private static async Task GetOrders()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("Orders with In Progress status");
            Console.WriteLine("------------------------------");
            try
            {
                var orders = await MerchantApi.ApiHelper.GetOrdersWithStatus();
                var table = new ConsoleTable("Id", "ChannelName", "ChannelOrderNo", "MerchantOrderNo", "Email", "Lines", "Status");
                orders.Content.ForEach(_ =>
                {
                    StringBuilder lines = new StringBuilder();
                    _.Lines.ForEach(x => lines.Append($"{x.Description},"));
                    table.AddRow(_.Id, _.ChannelName, _.ChannelOrderNo, _.MerchantOrderNo, _.Email, lines, _.Status);
                }
                );
                table.Write();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error during get order. {ex.Message}");
            }
            
        }
        private static void GetTop5Products()
        {
            try
            {
                Console.WriteLine("--------------");
                Console.WriteLine("Top 5 Products");
                Console.WriteLine("--------------");
                var products = MerchantApi.ApiHelper.GetTop5();
                var table = new ConsoleTable("ProductName", "Gtin", "Quantity");
                products.ForEach(_ => table.AddRow(_.ProductName, _.Gtin, _.Quantity));
                table.Write();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error during get top 5 products. {ex.Message}");
            }
}
        private static bool MainMenu()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("-----------------");
            Console.WriteLine(" 1) Update stock");
            Console.WriteLine(" 2) Exit");

            switch (Console.ReadLine())
            {
                case "1":
                    UpdateStock();
                    return true;
                case "2":
                    return false;
                default:
                    return true;
            }
        }

        private static void UpdateStock()
        {
            try
            {
                Console.WriteLine("Please Enter Product No:");
                var productNo = Console.ReadLine();
                Console.WriteLine("Please Enter Stock Location id:");
                int stockLocationId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(MerchantApi.ApiHelper.UpdateStock(productNo, 25, stockLocationId));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during Update Stock. {ex.Message}");
            }
        }
    }
}
