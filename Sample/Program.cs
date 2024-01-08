using TimeTrades;
using TimeTrades.StockAPIs;
using TimeTrades.StockAPIs.Contracts;

namespace Sample
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            StockAPI stockAPI = new AlphaStockAPI("123456789");
            var allSymbols = stockAPI.GetSupportedSymbols().ToList();
            stockAPI.SubscribeToSymbols(allSymbols);
            TimeTrader timeTrader = new TimeTrader(stockAPI);

            timeTrader.StockAPI.NewExchangeDataReceived += (sender, exchangeData) =>
            {
                Console.WriteLine(exchangeData.Str);
            };

            var cancellationTokenSource = await timeTrader.StockAPI.StartRealTimeDataReceiver(TimeSpan.FromSeconds(5));
            Console.WriteLine("Press any key to stop");
            Console.ReadKey();
            cancellationTokenSource.Cancel();
        }
    }
}
