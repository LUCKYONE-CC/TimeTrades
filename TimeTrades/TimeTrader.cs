using TimeTrades.Models;
using TimeTrades.StockAPIs.Contracts;

namespace TimeTrades
{
    public class TimeTrader
    {
        public StockAPI StockAPI { get; private set; }
        public TimeTrader(StockAPI stockAPI)
        {
            StockAPI = stockAPI;
        }
        public StockAPI GetBestAPI(List<StockAPI> apis, TimeSpan interval, List<Symbol> symbolsYouWantToWatch)
        {
            var bestApi = apis
                .Where(api => api.GetSupportedSymbols().Count() >= symbolsYouWantToWatch.Count())
                .Where(api => api.GetSupportedIntervals().Any(timeSpan => timeSpan == interval))
                .OrderBy(api => api.PricePerRequest)
                .FirstOrDefault();

            return StockAPI;
        }
    }
}
