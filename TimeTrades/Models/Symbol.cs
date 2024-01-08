namespace TimeTrades.Models
{
    public class Symbol
    {
        public Symbol(string code, string name, string type, string region, string currency, string isin)
        {
            Code = code;
            Name = name;
            Type = type;
            Region = region;
            Currency = currency;
            ISIN = isin;
        }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Region { get; private set; }
        public string Currency { get; private set; }
        public string ISIN { get; private set; }
    }
}
