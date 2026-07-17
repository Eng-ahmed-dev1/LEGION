namespace FantasyFootball.Domain.ValueObjects
{
    public record Price
    {
        public decimal Value { get; }
        public Price(decimal value)
        {
            if (value < 4.0m || value > 15.0m)
                throw new ArgumentException("Price must be between 4.0 and 15.0");
            Value = value;
        }
        public override string ToString() => $"{Value}";
    }
}
