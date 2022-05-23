namespace Common.ProgressBar
{
    using Konsole;

    public class EtaProgressBar
    {
        private readonly int total;
        private readonly int refreshRate;

        private int processed;
        private int nextProcessed;

        private long startTimestamp;
        private readonly ProgressBar defaultProgressBar;

        public EtaProgressBar(int total, int refreshRate)
        {
            this.total = total;
            this.refreshRate = refreshRate;
            this.defaultProgressBar = new ProgressBar(total);
        }

        public EtaProgressBar(int total) : this(total, total / 100) { }

        public void StartTimer()
        {
            processed = 0;
            nextProcessed = 0;
            startTimestamp = GetNowTimestamp();
        }

        public void AddAndRefresh(int value, string label)
        {
            Interlocked.Add(ref processed, value);
            if (processed >= nextProcessed)
            {
                Refresh(processed, label);
                Interlocked.Add(ref nextProcessed, refreshRate);
            }
        }

        private void Refresh(int value, string label)
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
