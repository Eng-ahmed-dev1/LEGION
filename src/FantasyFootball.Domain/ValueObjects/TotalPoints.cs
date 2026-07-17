namespace FantasyFootball.Domain.ValueObjects
{
    public record TotalPoints
    {
        public int Point { get; }
        public TotalPoints(int point)
        {
            if (point < 0)
                throw new ArgumentException("Total Point must be Positive");

            Point = point;
        }
        public override string ToString() => $"{Point}";

    }
}
