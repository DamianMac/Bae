namespace Bae
{
    public class GuessResult
    {
        public GuessResult(string label, double score)
        {
            this.Label = label;
            this.Score = score;

        }
        public string Label { get; set; }
        public double Score { get; set; }
    }
}