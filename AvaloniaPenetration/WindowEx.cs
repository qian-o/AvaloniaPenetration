using System;
using Avalonia.Controls;

namespace AvaloniaPenetration;

internal static partial class WindowEx
{
    public static void Penetration(this Window window)
    {
        nint handle = window.TryGetPlatformHandle()!.Handle;

        if (OperatingSystem.IsWindows())
        {
            Win32.Penetration(handle);
        }
        else if (OperatingSystem.IsLinux())
        {
            X11.Penetration(handle);
        }
        else
        {
            throw new PlatformNotSupportedException("This platform is not supported.");
        }
    }
}
