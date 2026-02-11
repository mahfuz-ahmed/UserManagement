using Serilog;
using System.Diagnostics;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/metrics.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Information("System Health Monitoring Started (Press Ctrl+C to stop)...");

var process = Process.GetCurrentProcess();
var lastCpuTime = process.TotalProcessorTime;
var stopwatch = Stopwatch.StartNew();

while (true)
{
    Thread.Sleep(10000);
    
    var currentCpuTime = process.TotalProcessorTime;
    var elapsed = stopwatch.Elapsed;
    stopwatch.Restart();

    // Calculate CPU usage as a percentage of total system resources used by this process over the interval
    var cpuUsedMs = (currentCpuTime - lastCpuTime).TotalMilliseconds;
    var totalMsPassed = elapsed.TotalMilliseconds;
    var cpuUsage = (cpuUsedMs / (Environment.ProcessorCount * totalMsPassed)) * 100;
    
    var memoryUsage = process.WorkingSet64 / (1024 * 1024); // MB

    Log.Information("{Timestamp:yyyy-MM-dd HH:mm:ss} | Metrics - CPU: {CpuUsage:F2}%, Memory: {MemoryUsage} MB", 
        DateTime.Now, cpuUsage, memoryUsage);

    lastCpuTime = currentCpuTime;
}
