using System;
using System.Runtime.InteropServices;

namespace AvaloniaPenetration;

public static partial class Win32
{
    [LibraryImport("USER32.dll")]
    private static partial int GetWindowLongPtrA(nint hWnd, int nIndex);

    [LibraryImport("USER32.dll")]
    private static partial int SetWindowLongPtrA(nint hWnd, int nIndex, int dwNewLong);

    [LibraryImport("USER32.dll")]
    private static partial int SetLayeredWindowAttributes(nint hwnd, uint crKey, byte bAlpha, uint dwFlags);

    public static void Penetration(nint handle)
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
}
