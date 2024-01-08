using TimeTrades.StockAPIs.Contracts;

namespace TimeTrades
{
    public delegate void InteractionHandler(object sender, ExchangeData exchangeData);
    public class TimeTrader
    {
        public event InteractionHandler NewExchangeDataReceived;
        public IStockAPI StockAPI { get; private set; }
        public TimeTrader(IStockAPI stockAPI)
        {
            StockAPI = stockAPI;

        }
        private async Task SendNewData(ExchangeData exchangeData)
        {
            if (NewExchangeDataReceived != null)
            {
                NewExchangeDataReceived(this, exchangeData);
            }
        }
    }
}
