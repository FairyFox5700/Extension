using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Build.Utilities;

namespace Extension.Interuptions
{
    class UserMouse:IDisposable
    {
        public event EventHandler<EventArgs> MouseMoved;
        private WindowsNativeMethods.HookProc MouseProc;
        private IntPtr MouseHandle;
        public UserMouse()
        {
            MouseProc = MouseCallBack;
            MouseHandle = WindowsNativeMethods.SetWindowsHookEx(HookCodes.HookType.WH_MOUSE_LL,MouseProc, IntPtr.Zero, 0);
        }

        private IntPtr MouseCallBack(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
                return WindowsNativeMethods.CallNextHookEx(MouseHandle, code, wParam, lParam);
            MouseMoved?.Invoke(this, new EventArgs());
            return WindowsNativeMethods.CallNextHookEx(MouseHandle, code, wParam, lParam);

        }

        #region IDisposable Support
        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (MouseHandle != IntPtr.Zero)
                {
                   
                    WindowsNativeMethods.UnhookWindowsHookEx(MouseHandle);
                }

                disposedValue = true;
            }
        }

         ~UserMouse()
        {

         Dispose(false);
         }

        public void Dispose()
        {
            Dispose(true);

        }
        #endregion
    }
}
