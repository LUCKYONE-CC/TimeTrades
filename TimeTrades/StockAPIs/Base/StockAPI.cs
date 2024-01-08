using TimeTrades.Models;

namespace TimeTrades.StockAPIs.Contracts
{
    public delegate void InteractionHandler(object sender, ExchangeData exchangeData);
    public abstract class StockAPI
    {
        public StockAPI(string apiKey)
        {
            APIKey = apiKey;
        }
        public event InteractionHandler NewExchangeDataReceived;
        protected string APIKey { get; }
        public abstract List<Symbol> SupportedSymbols { get; }
        public abstract List<TimeSpan> SupportedIntervals { get; }
        protected void OnNewExchangeDataReceived(ExchangeData exchangeData)
        {
            if (NewExchangeDataReceived != null)
            {
                NewExchangeDataReceived(this, exchangeData);
            }
        }
        public abstract IEnumerable<Symbol> GetSupportedSymbols();
        public abstract IEnumerable<TimeSpan> GetSupportedIntervals();
        public abstract void SubscribeToSymbols(List<Symbol> symbolsToSubscribe);
        protected abstract ExchangeData GetExchangeData(Symbol symbol);
        public async Task<CancellationToken> Start(TimeSpan timeSpan)
        {
            CancellationToken cancellationToken = new CancellationToken();
            if (GetSupportedIntervals().Any(x => x != timeSpan))
            {
                throw new ArgumentException("Interval is not supported on this api");
            }
            await Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if(SupportedSymbols == null || SupportedSymbols.Count == 0)
                    {
                        throw new ArgumentException("No Symbols subscribed");
                    }
                    foreach(var symbol in SupportedSymbols)
                    {
                        OnNewExchangeDataReceived(new ExchangeData());
                    }
                    Task.Delay(timeSpan);
                }
            });

            return cancellationToken;
        }
    }
}
