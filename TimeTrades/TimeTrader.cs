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
    }
}
