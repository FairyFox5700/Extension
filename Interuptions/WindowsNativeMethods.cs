using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Extension.Interuptions
{
    internal static class WindowsNativeMethods
    {


        /// <summary>
        ///     The CallWndProc hook procedure is an application-defined or library-defined
        ///     callback function used with the SetWindowsHookEx function. The HOOKPROC type
        ///     defines a pointer to this callback function. CallWndProc is a placeholder for
        ///     the application-defined or library-defined function name.
        /// </summary>
        /// <param name="nCode">
        ///     Specifies whether the hook procedure must process the message.
        /// </param>
        /// <param name="wParam">
        ///     Specifies whether the message was sent by the current thread.
        /// </param>
        /// <param name="lParam">
        ///     Pointer to a CWPSTRUCT structure that contains details about the message.
        /// </param>
        /// <returns>
        ///     If nCode is less than zero, the hook procedure must return the value returned
        ///     by CallNextHookEx. If nCode is greater than or equal to zero, it is highly
        ///     recommended that you call CallNextHookEx and return the value it returns;
        ///     otherwise, other applications that have installed WH_CALLWNDPROC hooks will
        ///     not receive hook notifications and may behave incorrectly as a result. If the
        ///     hook procedure does not call CallNextHookEx, the return value should be zero.
        /// </returns>
        internal delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        
            /// <summary>
            ///     The SetWindowsHookEx function installs an application-defined hook
            ///     procedure into a hook chain. You would install a hook procedure to monitor
            ///     the system for certain types of events. These events are associated either
            ///     with a specific thread or with all threads in the same desktop as the
            ///     calling thread.
            /// </summary>
            /// <param name="hookType">
            ///     Specifies the type of hook procedure to be installed
            /// </param>
            /// <param name="callback">Pointer to the hook procedure.</param>
            /// <param name="hMod">
            ///     Handle to the DLL containing the hook procedure pointed to by the lpfn
            ///     parameter. The hMod parameter must be set to NULL if the dwThreadId
            ///     parameter specifies a thread created by the current process and if the
            ///     hook procedure is within the code associated with the current process.
            /// </param>
            /// <param name="dwThreadId">
            ///     Specifies the identifier of the thread with which the hook procedure is
            ///     to be associated.
            /// </param>
            /// <returns>
            ///     If the function succeeds, the return value is the handle to the hook
            ///     procedure. If the function fails, the return value is 0.
            /// </returns>
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SetWindowsHookEx(HookCodes.HookType hookType,
                HookProc callback, IntPtr hMod, uint dwThreadId);

            /// <summary>
            ///     The UnhookWindowsHookEx function removes a hook procedure installed in
            ///     a hook chain by the SetWindowsHookEx function.
            /// </summary>
            /// <param name="hhk">Handle to the hook to be removed.</param>
            /// <returns>
            ///     If the function succeeds, the return value is true.
            ///     If the function fails, the return value is false.
            /// </returns>
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern bool UnhookWindowsHookEx(IntPtr hhk);

            /// <summary>
            ///     The CallNextHookEx function passes the hook information to the next hook
            ///     procedure in the current hook chain. A hook procedure can call this
            ///     function either before or after processing the hook information.
            /// </summary>
            /// <param name="idHook">Handle to the current hook.</param>
            /// <param name="nCode">
            ///     Specifies the hook code passed to the current hook procedure.
            /// </param>
            /// <param name="wParam">
            ///     Specifies the wParam value passed to the current hook procedure.
            /// </param>
            /// <param name="lParam">
            ///     Specifies the lParam value passed to the current hook procedure.
            /// </param>
            /// <returns>
            ///     This value is returned by the next hook procedure in the chain.
            /// </returns>
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
                IntPtr wParam, IntPtr lParam);
        }

        



}
