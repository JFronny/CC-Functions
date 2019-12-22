using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CC_Functions.W32.Hooks
{
    public sealed class KeyboardHook : IDisposable
    {
        public delegate void KeyPress(KeyboardHookEventArgs args);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        private static readonly List<KeyboardHook> Instances = new List<KeyboardHook>();
        private static readonly LowLevelKeyboardProc Proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        public KeyboardHook()
        {
            if (Instances.Count == 0)
                _hookID = SetHook(Proc);
            Instances.Add(this);
        }

        public void Dispose()
        {
            Instances.Remove(this);
            if (Instances.Count == 0)
                UnhookWindowsHookEx(_hookID);
        }

        public event KeyPress OnKeyPress;

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr) WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                for (int i = 0; i < Instances.Count; i++)
                    Instances[i].OnKeyPress?.Invoke(new KeyboardHookEventArgs((Keys) vkCode));
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod,
            uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
    }
}