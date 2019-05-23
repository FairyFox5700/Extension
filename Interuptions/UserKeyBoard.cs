using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension.Interuptions
{
    class UserKeyBoard:IDisposable
    {
        public event EventHandler<EventArgs> KeyBoardPressed;
        private WindowsNativeMethods.HookProc KeyBoardProc;
        private IntPtr KeyBoardHandle;

        public UserKeyBoard()
        {
            KeyBoardProc = KeyBoardCallBack;
            KeyBoardHandle = WindowsNativeMethods.SetWindowsHookEx(HookCodes.HookType.WH_KEYBOARD_LL, KeyBoardProc, IntPtr.Zero, 0);
        }

        private IntPtr KeyBoardCallBack(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
                return WindowsNativeMethods.CallNextHookEx(KeyBoardHandle, code, wParam, lParam);
            KeyBoardPressed?.Invoke(this, new EventArgs());
            return WindowsNativeMethods.CallNextHookEx(KeyBoardHandle, code, wParam, lParam);

        }
        #region IDisposable Support
        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (KeyBoardHandle != IntPtr.Zero)
                {
                    WindowsNativeMethods.UnhookWindowsHookEx(
                        KeyBoardHandle); 
                }
                disposedValue = true;
            }
        }

        ~UserKeyBoard()
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
