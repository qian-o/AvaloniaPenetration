using System;
using System.Runtime.InteropServices;
using Avalonia.Controls;

namespace AvaloniaPenetration;

internal static partial class WindowEx
{
    [LibraryImport("USER32.dll")]
    private static partial int GetWindowLongPtrA(nint hWnd, int nIndex);

    [LibraryImport("USER32.dll")]
    private static partial int SetWindowLongPtrA(nint hWnd, int nIndex, int dwNewLong);

    [LibraryImport("USER32.dll")]
    private static partial int SetLayeredWindowAttributes(nint hwnd, uint crKey, byte bAlpha, uint dwFlags);

    [LibraryImport("libX11.so.6", StringMarshalling = StringMarshalling.Utf8)]
    public static partial nint XOpenDisplay(string display);

    [LibraryImport("libX11.so.6")]
    public static partial void XCloseDisplay(nint display);

    [LibraryImport("libX11.so.6")]
    public static partial nint XCreateRegion();

    [LibraryImport("libXext.so.6")]
    public static partial void XShapeCombineRegion(nint display, nint window, int destKind, int xOff, int yOff, nint region, int op);

    public static void Penetration(this Window window)
    {
        nint handle = window.TryGetPlatformHandle()!.Handle;

        if (OperatingSystem.IsWindows())
        {
            const int GWL_EXSTYLE = -20;
            const int WS_EX_LAYERED = 0x00080000;
            const int WS_EX_TRANSPARENT = 0x00000020;
            const int LWA_ALPHA = 0x00000002;

            if (SetWindowLongPtrA(handle, GWL_EXSTYLE, GetWindowLongPtrA(handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_TRANSPARENT) is 0)
            {
                throw new Exception("SetWindowLongPtrA failed.");
            }

            if (SetLayeredWindowAttributes(handle, 0, 255, LWA_ALPHA) is 0)
            {
                throw new Exception("SetLayeredWindowAttributes failed.");
            }
        }
        else if (OperatingSystem.IsLinux())
        {
            nint display = XOpenDisplay(string.Empty);

            XShapeCombineRegion(display, handle, 2, 0, 0, XCreateRegion(), 0);

            XCloseDisplay(display);
        }
        else
        {
            throw new PlatformNotSupportedException("This platform is not supported.");
        }
    }
}
