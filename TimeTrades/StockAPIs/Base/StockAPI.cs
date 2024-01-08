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
        protected abstract bool HasStarted { get; set; }
        public abstract decimal PricePerRequest { get; }
        protected abstract IEnumerable<Symbol> SubscribedSymbols { get; set; }
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
        protected abstract ExchangeData GetRealTimeExchangeData(Symbol symbol);
        public async Task<CancellationTokenSource> StartRealTimeDataReceiver(TimeSpan timeSpan)
        {
            try
            {
                if(HasStarted)
                {
                    throw new ArgumentException("API has already started");
                }

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                CancellationToken token = cancellationTokenSource.Token;
                var supportedIntervals = GetSupportedIntervals().ToList();

                if (supportedIntervals == null || supportedIntervals.Count == 0)
                {
                    throw new ArgumentException("No Intervals supported");
                }

                if (timeSpan == null || timeSpan == TimeSpan.Zero)
                {
                    throw new ArgumentException("TimeSpan cannot be null or zero");
                }

                if (supportedIntervals.All(x => x != timeSpan))
                {
                    throw new ArgumentException("TimeSpan not supported");
                }
                Task apiTask = new Task(async () =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        if (SubscribedSymbols == null || SubscribedSymbols.ToList().Count == 0)
                        {
                            throw new ArgumentException("No Symbols subscribed");
                        }
                        foreach (var symbol in SubscribedSymbols)
                        {
                            var exchangeData = GetRealTimeExchangeData(symbol);
                            OnNewExchangeDataReceived(exchangeData);
                        }
                        await Task.Delay(timeSpan);
                    }
                    HasStarted = false;
                });

                apiTask.Start();

                return cancellationTokenSource;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
