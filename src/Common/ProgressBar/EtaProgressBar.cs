namespace Common.ProgressBar
{
    using Konsole;

    public class EtaProgressBar
    {
        private readonly int total;
        private long startTimestamp;
        private readonly ProgressBar defaultProgressBar;

        public EtaProgressBar(int total)
        {
            this.total = total;
            this.defaultProgressBar = new ProgressBar(total);
        }

        public void StartTimer()
        {
            startTimestamp = GetNowTimestamp();
        }

        public void Refresh(int value, string label)
        {
            long taken = GetNowTimestamp() - startTimestamp;
            float eta = ((float)taken / value) * (total - value);
            string etaString = String.Format("{0:F2}", eta / 1000f);
            string withTimeLabel = $"{label} (~ {etaString}s)";
            defaultProgressBar.Refresh(value, withTimeLabel);
        }

        private long GetNowTimestamp()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        }
    }
}
