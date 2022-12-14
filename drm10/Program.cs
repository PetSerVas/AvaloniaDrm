using Avalonia;
using System;
using System.Linq;
using System.Threading;

namespace drm10;

class Program
{
    [STAThread]
    public static int Main(string[] args) 
    {
        var builder = BuildAvaloniaApp();
        if (args.Contains("--drm"))
        {
            SilenceConsole();
            return builder.StartLinuxDrm(args);
        }
        return builder.StartWithClassicDesktopLifetime(args);

    }

    private static void SilenceConsole()
    {
        new Thread(() =>
        {
            if (!Console.IsInputRedirected && Console.KeyAvailable)
            {
                Console.CursorVisible = false;
                while (true)
                    Console.ReadKey();
            }
            else
            {
                while (true)
                    Console.Read();
            }

        })
        { IsBackground = true }.Start();
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
}
