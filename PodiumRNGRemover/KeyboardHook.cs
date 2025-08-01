using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PodiumRNGRemover
{
    public class KeyboardHook : IDisposable
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        
        private LowLevelKeyboardProc proc;
        private IntPtr hookID = IntPtr.Zero;
        private System.Collections.Generic.HashSet<Keys> registeredKeys = new System.Collections.Generic.HashSet<Keys>();
        
        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        public KeyboardHook()
        {
            proc = HookCallback;
        }

        public void RegisterHotKey(Keys key)
        {
            registeredKeys.Add(key);
            if (hookID == IntPtr.Zero)
            {
                hookID = SetHook(proc);
            }
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                
                Keys pressedKey = (Keys)vkCode;
                if (registeredKeys.Contains(pressedKey))
                {
                    KeyPressed?.Invoke(this, new KeyPressedEventArgs(pressedKey));
                }
            }

            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            if (hookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(hookID);
                hookID = IntPtr.Zero;
            }
            
            registeredKeys?.Clear();
            proc = null;
            KeyPressed = null;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }

    public class KeyPressedEventArgs : EventArgs
    {
        public Keys Key { get; }

        public KeyPressedEventArgs(Keys key)
        {
            Key = key;
        }
    }
}