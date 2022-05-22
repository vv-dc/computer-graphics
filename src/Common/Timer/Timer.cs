namespace Common.Timer
{
    public class Timer
    {

        public static void LogTime(Action fn, string label)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            fn();
            watch.Stop();
            Console.WriteLine($"{label} took: {watch.ElapsedMilliseconds / 1000f}s");
        }
    }
}
