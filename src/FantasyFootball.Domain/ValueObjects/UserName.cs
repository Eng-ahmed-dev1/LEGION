namespace FantasyFootball.Domain.ValueObjects
{
    public record UserName
    {
        public string Value { get; }
        public UserName(string value)
        {
            value = value.Trim();
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Username is required");

            if (value.Length < 3 || value.Length > 20)
                throw new Exception("Username must be 3-20 characters");

            if (!value.All(c => char.IsLetterOrDigit(c) || c == '_'))
                throw new Exception("Invalid username");

            Value = value;
        }
        public override string ToString() => Value;
    }
}
