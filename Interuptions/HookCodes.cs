using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Extension.Interuptions
{
    internal static  class HookCodes
    {
        
            public const int HC_ACTION = 0;
            public const int HC_GETNEXT = 1;
            public const int HC_SKIP = 2;
            public const int HC_NOREMOVE = 3;
            public const int HC_NOREM = HC_NOREMOVE;
            public const int HC_SYSMODALON = 4;
            public const int HC_SYSMODALOFF = 5;
        

        internal enum HookType
        {
            WH_KEYBOARD = 2,
            WH_MOUSE = 7,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class POINT
        {
            public int x;
            public int y;
        }

        /// <summary>
        ///     The MSLLHOOKSTRUCT structure contains information about a low-level keyboard
        ///     input event.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEHOOKSTRUCT
        {
            public POINT pt; // The x and y coordinates in screen coordinates
            public int hwnd; // Handle to the window that'll receive the mouse message
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        /// <summary>
        ///     The MOUSEHOOKSTRUCT structure contains information about a mouse event passed
        ///     to a WH_MOUSE hook procedure, MouseProc.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MSLLHOOKSTRUCT
        {
            public POINT pt; // The x and y coordinates in screen coordinates. 
            public int mouseData; // The mouse wheel and button info.
            public int flags;
            public int time; // Specifies the time stamp for this message. 
            public IntPtr dwExtraInfo;
        }

        internal enum MouseMessage
        {
            WM_MOUSEMOVE = 0x0200,
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_LBUTTONDBLCLK = 0x0203,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_RBUTTONDBLCLK = 0x0206,
            WM_MBUTTONDOWN = 0x0207,
            WM_MBUTTONUP = 0x0208,
            WM_MBUTTONDBLCLK = 0x0209,

            WM_MOUSEWHEEL = 0x020A,
            WM_MOUSEHWHEEL = 0x020E,

            WM_NCMOUSEMOVE = 0x00A0,
            WM_NCLBUTTONDOWN = 0x00A1,
            WM_NCLBUTTONUP = 0x00A2,
            WM_NCLBUTTONDBLCLK = 0x00A3,
            WM_NCRBUTTONDOWN = 0x00A4,
            WM_NCRBUTTONUP = 0x00A5,
            WM_NCRBUTTONDBLCLK = 0x00A6,
            WM_NCMBUTTONDOWN = 0x00A7,
            WM_NCMBUTTONUP = 0x00A8,
            WM_NCMBUTTONDBLCLK = 0x00A9
        }

        /// <summary>
        ///     The structure contains information about a low-level keyboard input event.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct KBDLLHOOKSTRUCT
        {
            public int vkCode; // Specifies a virtual-key code
            public int scanCode; // Specifies a hardware scan code for the key
            public int flags;
            public int time; // Specifies the time stamp for this message
            public int dwExtraInfo;
        }

        internal enum KeyboardMessage
        {
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101,
            WM_SYSKEYDOWN = 0x0104,
            WM_SYSKEYUP = 0x0105
        }
   

        /// <summary>
        /// Helps to find the idle time, (in milliseconds) spent since the last user input
        /// </summary>
        
    }
}

