﻿namespace HeatManagement;
using Avalonia;
using System;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        if (args.Length > 0)
            switch (args[0])
            {
                case "--help":
                case "-h":
                    CLI.PrintHelp();
                    break;
                case "--cli":
                case "--console":
                case "-c":
                    args = args[1..];
                    CLI.Run(args);
                    break;
                case "--gui":
                case "--avalonia":
                case "-g":
                    args = args[1..];
                    BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
                    break;
                default:
                    BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
                    break;
            }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();

    static void OnProcessExit(object? sender, EventArgs e)
    {
        Console.CursorVisible = true;
    }
}
