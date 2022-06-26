namespace Common.ProgressBar
{
    using Konsole;

    public class EtaProgressBar
    {
        private const int REFRESH_COUNT = 100;
        private const string FORMAT = "{0}: (~{1:F2}s)";
        private static object locker = new object();

        private readonly int refreshRate;
        private int current;
        private string label;
        private int updates;
        private long startTime;

        private readonly ProgressBar bar;

        public EtaProgressBar(int total, string label, int refreshRate)
        {
            this.bar = new ProgressBar(total);
            this.label = label;
            this.refreshRate = refreshRate;
            this.current = 0;
            this.updates = 1;
        }

        public EtaProgressBar(int total, string label) : this(total, label, total / REFRESH_COUNT) { }

        public void Next(int step = 1)
        {
            lock (locker)
            {
                current = Math.Min(current + step, bar.Max);
                if (current >= updates * refreshRate)
                {
                    Update(current);
                    ++updates;
                }
            }
        }

        private void Update(int value)
        {
            if (startTime == default(long))
            {
                startTime = GetNow();
                bar.Refresh(value, label);
            }
            else
            {
                float updateTime = (GetNow() - startTime) / (float)(value - 1);
                float eta = updateTime * (bar.Max - value);
                bar.Refresh(value, FORMAT, new object[] { label, eta });
            }
        }

        private long GetNow()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }
    }
}
