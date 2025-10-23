using System.Runtime.InteropServices;

namespace AvaloniaPenetration;

public static partial class X11
{
    [LibraryImport("libX11.so.6")]
    private static partial nint XOpenDisplay(nint displayName);

    [LibraryImport("libX11.so.6")]
    private static partial void XCloseDisplay(nint display);

    [LibraryImport("libX11.so.6")]
    private static partial nint XCreateRegion();

    [LibraryImport("libXext.so.6")]
    private static partial void XShapeCombineRegion(nint display, nint window, int destKind, int xOff, int yOff, nint region, int op);

    public static void Penetration(nint handle)
    {
        nint display = XOpenDisplay(0);

        XShapeCombineRegion(display, handle, 2, 0, 0, XCreateRegion(), 0);

        XCloseDisplay(display);
    }
}
